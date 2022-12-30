# Telegraph Connector!

| Build | Latest Version | TelegraphConnector | TelegraphConnector.Parses |
|--------|--------|--------|-------|
| [![Build](https://github.com/MarcosCostaDev/TelegraphConnector/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/MarcosCostaDev/TelegraphConnector/actions/workflows/build-and-test.yml)| [![GitHub tag (latest by date)](https://img.shields.io/github/v/tag/marcoscostadev/TelegraphConnector)](https://github.com/MarcosCostaDev/TelegraphConnector/tags) | [![Nuget](https://img.shields.io/nuget/v/TelegraphConnector)](https://www.nuget.org/packages/TelegraphConnector/) | [![Nuget](https://img.shields.io/nuget/v/TelegraphConnector.Parses?logo=%20)](https://www.nuget.org/packages/TelegraphConnector.Parses/) |

 


Introducing TelegraphConnector - the Nuget package that makes it easy to communicate with the [Telegra.ph API](https://telegra.ph/api). With this package, you can easily create and publish web pages using resources from [Telegra.ph](https://telegram.org/blog/telegraph), giving you access to a wide range of features and functionality.

Whether you want to publish articles, or share multimedia content, TelegraphConnector makes it simple and straightforward to do so. Simply install the package, create or authenticate with your Telegra.ph account, and start building your web page for use in the Telegram instant view feature.

With easy-to-use methods and comprehensive documentation, TelegraphConnector is the perfect choice for anyone looking to utilize the power of Telegra.ph.


## Installation

To install TelegraphConnector, run the following command in the Package Manager Console:

```
Install-Package TelegraphConnector
```
Or use the `dotnet cli`:

```
dotnet add package TelegraphConnector
```

## Usage

First, Create an account using the Authenticate method. Then, use the various methods provided by the package to create and publish your web pages.

### Create an Account

```csharp
using TelegraphConnector.Types;
using TelegraphConnector.Services;

// Create an account
 Account account = Account.Create("My Name", "My Full Name", "https://github.com/MarcosCostaDev/TelegraphConnector");

// AccountService is used for accessing the Telegraph API
 AccountService accountService = new AccountService();

// Every service returns a TelegraphResponse that contains a 
// Result property with the object listed in the types TelegraphConnector.Types 
// Accordingly with the service called.
// The object that represents your account will be in "response.Result"
 TelegraphResponse<Account> response = await accountService.CreateAccountAsync(account);
```

After create your account, keep the `response.Result.AccessToken` in a save place. The `AccessToken` will be useful for the other methods.

### Create a Hello World Page
```csharp
// Create a text node that is used for writing a single text in your page
Node helloWorldText = Node.CreateTextNode("Hello World");

// Initiate a array of nodes, you can send multiple notes to create and edit a page in the telegaph
Node[] nodes = new Node[] {
      Node.CreateParagraph(helloWorldText)
};

// use your account object received in the 'Create an Account' section
// the 'true' value here will return the entire object represented your page when you call the Page Service
Page page = Page.Create(account, "Hello World Page", nodes, true);

// Initialize a Page Service that will manage your pages in the telegraph
PageService pageService = new PageService();

// Create a page in the Telegraph.
TelegraphResponse<Page> createdPage = await pageService.CreatePageAsync(account.AccessToken, page);

```

### Types

All the types are listed in `TelegraphConnector.Types` that are a representation of [available types](https://telegra.ph/api#Available-types) in the telegraph api:

- [Account](https://telegra.ph/api#Account) - This object represents a Telegraph account. 
- [Node](https://telegra.ph/api#Node) - This abstract object represents a DOM Node. It can be a String which represents a DOM text node or a NodeElement object.
- [Page](https://telegra.ph/api#Page) - This object represents a page on Telegraph.
- [PageList](https://telegra.ph/api#PageList) - This object represents a list of Telegraph articles belonging to an account. Most recently created articles first.
- [PageViews](https://telegra.ph/api#PageViews) - This object represents the number of page views for a Telegraph article.


## Telegraph Connector Parses !

Enhance your Telegra.ph experience with TelegraphConnector.Parses - the extension for TelegraphConnector that allows you to import HTML and Markdown files for use within the TelegraphConnector package.

With this extension, you can easily incorporate external content into your web page, giving you even more flexibility and control. Simply install the extension, authenticate with your Telegra.ph account, and start importing your files.

Whether you want to incorporate existing content or create something new, TelegraphConnector.Parse makes it easy to do so. With its easy-to-use methods and comprehensive documentation, TelegraphConnector.Parse is a must-have for anyone using TelegraphConnector.


## Installation

To install TelegraphConnector.Parses, run the following command in the Package Manager Console:

```
Install-Package TelegraphConnector.Parses
```
Or use the `dotnet cli`:

```
dotnet add package TelegraphConnector.Parses
```


### HTML Parse

```csharp
  using TelegraphConnector.Parses;
  using TelegraphConnector.Services;
  using TelegraphConnector.Types;

  string html = @"
   <html>
   <head>
   </head>
   <body>
       <p class='some-class' data-id='123'>Hello World!</p>
   </body>
   </html>
  ";

  // Get the nodes based on html text
  Node[] nodes = TelegraphHtml.Parse(html).ToArray();

  // use your account object received in the 'Create an Account' section
  // the 'true' value here will return the entire object represented your page when you call the Page Service
  Page page = Page.Create(account, "Hello World Page", nodes, true);

  // Initialize a Page Service that will manage your pages in the telegraph
  PageService pageService = new PageService();

  // Create a page in the Telegraph.
  TelegraphResponse<Page> createdPage = await pageService.CreatePageAsync(account.AccessToken, page);
```


### Markdown Parse


```csharp
  using TelegraphConnector.Parses;
  using TelegraphConnector.Services;
  using TelegraphConnector.Types;

  string markdown = @"
    # Hello World

    Hello World Paragraph.
  ";

  // Get the nodes based on markdown text
  Node[] nodes = TelegraphMarkdown.Parse(markdown).ToArray();

  // use your account object received in the 'Create an Account' section
  // the 'true' value here will return the entire object represented your page when you call the Page Service
  Page page = Page.Create(account, "Hello World Page", nodes, true);

  // Initialize a Page Service that will manage your pages in the telegraph
  PageService pageService = new PageService();

  // Create a page in the Telegraph.
  TelegraphResponse<Page> createdPage = await pageService.CreatePageAsync(account.AccessToken, page);
```

## Preview Versions

If you want to install the most recent preview versions, create a `NuGet.config` file and place it into your solution.
Use the path `[your-solution-dir]/.nuget/nuget.Config` or `[your-solution-dir]/nuget.Config`

```xml

<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <!--To inherit the global NuGet package sources remove the <clear/> line below -->
    <clear />
    <add key="nuget" value="https://api.nuget.org/v3/index.json" />
    <!-- Add this line -->
    <add key="github" value="https://nuget.pkg.github.com/marcoscostadev/index.json" /> 
  </packageSources>
</configuration>

```

## Support 

For any questions or issues, please create an [issue](https://github.com/MarcosCostaDev/TelegraphConnector/issues/new) in this repository.
