# Portfolio To Do

- [Portfolio To Do](#portfolio-to-do)
  - [Client App - Portfolio To Do](#client-app---portfolio-to-do)
  - [<h3 id="security-478">Security</h3>](#h3-id%22security-478%22securityh3)
  - [Backend (To Do)](#backend-to-do)

## Client App - [Portfolio To Do](#portfolio-to-do)
  
### Security
---
- [ ] Setup User Secrets 

  `Initialize` User Secret Store:
    ```bash
    dotnet user-secrets init
    ```
      
  Secrets Located (Check .csproj for <user_secrets_id>):
    ```
    ~/.microsoft/usersecrets/<user_secrets_id>/secrets.json
    ```

    `CRUD` operations on a Secret:
    ```bash
    #create
    dotnet user-secrets set "Portfolio:Database:DbPaxxword" "reallyStrongPwd123" 

    #delete
    dotnet user-secrets remove "Portfolio:Database:DbPaxxword"

    #list
    dotnet user-secrets list
    ```


See Also:

[Safe storage of app secrets in development in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=linux#how-the-secret-manager-tool-works)


---
- [ ] Create a Modal Center Login Full Screen

See:

[Modal Login Form Wordpress by Tyler Fry](https://codepen.io/frytyler/pen/EGdtg)\
[^^^ Full Screen View](https://codepen.io/frytyler/full/EGdtg)

---
- [ ] Add Following Line To `package.json`
```json
"babel-plugin-transform-imports": "^2.0.0",
"vue-template-compiler": "^2.6.10",
```

These ^^^ were in `"devDependencies: { "`

---


## Backend (To Do)