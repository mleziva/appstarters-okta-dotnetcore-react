Steps for running locally
1. Restore nuget packages
2. Confirm builds successfull
3. Update appsettings.json with correct OktaDomain

Steps for getting token:
1. Rename logindefault.html to login or fill in the secrets directly in that file
2. Add the redirect uri to okta app through the developer portal
3. go to https://localhost:44390/token/login.html and sign in and see access token