When working on software projects there will undoubtedly be a number of configuration items that will be required for various libraries extensions and  API's , typically these may require us to store application credentials, API keys, SSH keys, database passwords, encryption keys, and so on. access etc required.  In many cases these setting items will need to be kept secret, in that there values need to kept hidden but at the same time used through out the application.  

In dotnet applications developers will typically tend to store Configuration values in the JSON based text file in the root of their project directory typically named appsettings.json . The main problem with storing settings in this file is that they are typically stored in open text and can be easily read and shared with anyone.

```json
{
  "SomeService": {
    "Endpoint": "www.someservice.com",
    "Data": "SomeData",
    "Serial": "customer-512-717",
    "Password": "S0m3Cr@zyP4ssw0rd",
    "Username": "Someone@somewhere.com"
  }
}
```

If this file is ever accidentally committed to an version control repository, such as GitHub or Gitlab these values can then be read by anyone who can gain access to the repository. If this is a repository is a public repository then these values are open to the public.

The added problem if these values are ever mistakenly committed to a version control repository, and then quickly deleted their values will still be available in Git history file, and there values can be extracted by reviewing the history.

### Secret Management in dotnet

Fortunately the dotnet framework offers a few ways to help us to more effectively manage secrets with our applications. 

### Using Dotnet User Secrets

Instead of exposing our configuration variable values in the in the appsetting.json file, we could make use of the dotnet CLI functionality to add secrets to a secret repository on our machine and access them withing our code  without keeping them in the project folder.

If you have the Dotnet SDK installed on your machine the dotnet user-secrets is available by default. You simply call up the Help by open a terminal window and using the following command

```sh
dotnet user-secrets --help
```

Which will provide you with output that looks something similar to this screenshot below:

[image goes here]

Using these terminal commands you can view, set, edit and remove any secrets for any projects that you have defined secrets for locally.

In order to learn how to use secret Manager lets create a simple project, I'll be making use of API Template Pack to generate a new REST API project, which I simply create using the following command in terminal window. The full instructions on how to install and use the API Template Pack are available on Getting Started with the API Template Pack.

```sh
dotnet new apisolution -n AddressService --root Threenine
```

The command will generate a complete REST API project template with everything ready to start implementing REST Resource Based API's

[image goes here]


Navigate to the `appsettings.json` file in the root of the API project, then add the following Configuration values.  We will initially just set them all to empty.

[image goes here]

For the sake of this tutorial, we want to add some details relating to the endpoint but obviously we want to be able to store the Serial and Password details in the secrets. So we can update the details as follows and just leave the Serial and Password fields empty.

```json
{
  "AfdSettings": {
    "Endpoint": "www.someurl.com",
    "Data": "Address",
    "Serial": "",
    "Password": ""
  }
}
```

We can now add our secrets we'd like to keep in the secret store for this application using the following command in the terminal window

```sh
dotnet user-secrets add "AfdSettings_
```
