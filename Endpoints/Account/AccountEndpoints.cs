using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using EducationalInstitution.Models.DTO.Requests.Users;
using EducationalInstitution.Models.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace EducationalInstitution.Endpoints.Account;

/// <summary>
/// User account endpoint
/// </summary>
public static class AccountEndpoints
{
    private static readonly EmailAddressAttribute _emailAddressAttribute = new();

    /// <summary>
    /// Mapping custom user account endpoint provided by base identity endpoints
    /// </summary>
    /// <param name="endpoints"></param>
    /// <returns></returns>
    public static IEndpointConventionBuilder MapUserAccountEndpoints(
        this IEndpointRouteBuilder endpoints
    )
    {
        var emailSender = endpoints.ServiceProvider.GetRequiredService<
            IEmailSender<ApplicationUser>
        >();
        var linkGenerator = endpoints.ServiceProvider.GetRequiredService<LinkGenerator>();

        var routeGroup = endpoints.MapGroup("/account");
        routeGroup.MapIdentityApi<ApplicationUser>();

        endpoints
            .MapPost(
                "/account/v2/register",
                async Task<
                    Results<
                        Ok<AccessTokenResponse>,
                        EmptyHttpResult,
                        ProblemHttpResult,
                        ValidationProblem
                    >
                > (
                    [FromBody] RegisterUser registration,
                    [FromQuery] bool? useCookies,
                    [FromQuery] bool? useSessionCookies,
                    HttpContext context,
                    [FromServices] IServiceProvider sp
                ) =>
                {
                    var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();

                    if (!userManager.SupportsUserEmail)
                    {
                        throw new NotSupportedException(
                            $"{nameof(MapUserAccountEndpoints)} requires a user store with email support."
                        );
                    }

                    var userStore = sp.GetRequiredService<IUserStore<ApplicationUser>>();
                    var emailStore = (IUserEmailStore<ApplicationUser>)userStore;
                    var email = registration.Email;

                    if (string.IsNullOrEmpty(email) || !_emailAddressAttribute.IsValid(email))
                    {
                        return CreateValidationProblem(
                            IdentityResult.Failed(userManager.ErrorDescriber.InvalidEmail(email))
                        );
                    }

                    var user = new ApplicationUser()
                    {
                        FirstName = registration.FirstName,
                        LastName = registration.LastName,
                    };
                    await userStore.SetUserNameAsync(user, email, CancellationToken.None);
                    await emailStore.SetEmailAsync(user, email, CancellationToken.None);
                    var newUser = await userManager.CreateAsync(user, registration.Password);

                    if (!newUser.Succeeded)
                    {
                        return CreateValidationProblem(newUser);
                    }

                    var signInManager = sp.GetRequiredService<SignInManager<ApplicationUser>>();

                    var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
                    var isPersistent = (useCookies == true) && (useSessionCookies != true);
                    signInManager.AuthenticationScheme = useCookieScheme
                        ? IdentityConstants.ApplicationScheme
                        : IdentityConstants.BearerScheme;

                    var result = await signInManager.PasswordSignInAsync(
                        email,
                        registration.Password,
                        isPersistent,
                        lockoutOnFailure: true
                    );
                    if (!result.Succeeded)
                    {
                        return TypedResults.Problem(
                            result.ToString(),
                            statusCode: StatusCodes.Status401Unauthorized
                        );
                    }

                    //  await SendConfirmationEmailAsync(user, userManager, context, email);
                    return TypedResults.Empty;
                }
            )
            .WithDescription(
                "Modified version identity endpoints which includes more attributes for the user"
            );

        //  routeGroup.MapPost("/login", async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>>
        //         ([FromBody] LoginRequest login, [FromQuery] bool? useCookies, [FromQuery] bool? useSessionCookies, [FromServices] IServiceProvider sp) =>
        //     {
        //         var signInManager = sp.GetRequiredService<SignInManager<TUser>>();

        //         var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
        //         var isPersistent = (useCookies == true) && (useSessionCookies != true);
        //         signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

        //         var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, isPersistent, lockoutOnFailure: true);

        //         if (result.RequiresTwoFactor)
        //         {
        //             if (!string.IsNullOrEmpty(login.TwoFactorCode))
        //             {
        //                 result = await signInManager.TwoFactorAuthenticatorSignInAsync(login.TwoFactorCode, isPersistent, rememberClient: isPersistent);
        //             }
        //             else if (!string.IsNullOrEmpty(login.TwoFactorRecoveryCode))
        //             {
        //                 result = await signInManager.TwoFactorRecoveryCodeSignInAsync(login.TwoFactorRecoveryCode);
        //             }
        //         }

        //         if (!result.Succeeded)
        //         {
        //             return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
        //         }

        //         // The signInManager already produced the needed response in the form of a cookie or bearer token.
        //         return TypedResults.Empty;
        //     });

        return routeGroup;
    }

    private static ValidationProblem CreateValidationProblem(
        string errorCode,
        string errorDescription
    ) =>
        TypedResults.ValidationProblem(
            new Dictionary<string, string[]> { { errorCode, [errorDescription] } }
        );

    private static ValidationProblem CreateValidationProblem(IdentityResult result)
    {
        // We expect a single error code and description in the normal case.
        // This could be golfed with GroupBy and ToDictionary, but perf! :P
        Debug.Assert(!result.Succeeded);
        var errorDictionary = new Dictionary<string, string[]>(1);

        foreach (var error in result.Errors)
        {
            string[] newDescriptions;

            if (errorDictionary.TryGetValue(error.Code, out var descriptions))
            {
                newDescriptions = new string[descriptions.Length + 1];
                Array.Copy(descriptions, newDescriptions, descriptions.Length);
                newDescriptions[descriptions.Length] = error.Description;
            }
            else
            {
                newDescriptions = [error.Description];
            }

            errorDictionary[error.Code] = newDescriptions;
        }

        return TypedResults.ValidationProblem(errorDictionary);
    }
}
