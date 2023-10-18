using System.Security.Claims;


Claim emailClaim = new(ClaimTypes.Email, "brilmansander@gmail.com");
Claim usernameClaim = new(ClaimTypes.Name, "sander");

Claim[] claims = new[] { emailClaim, usernameClaim };

ClaimsIdentity identity = new ClaimsIdentity(claims, "qwdqw");




Console.WriteLine(identity.IsAuthenticated);

Console.ReadLine();
