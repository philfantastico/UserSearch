using UserSearch;
using UserSearch.Models;
using UserSearch.Repositories;
using System;
using System.Web.Mvc;
using UserSearch.WebClients;
using System.Text.RegularExpressions;

namespace UserSearchWeb.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Default controller serves the main page for the User Search application
        /// </summary>
        /// <param name="loginname">
        /// A source control user's login name used to search in a source control system for 
        /// details of the user. Validated against a whitelist of allowed characters on both the 
        /// client and the server to help guard against injection attacks.
        /// </param>
        /// <returns>
        /// The main page of the User Search application including 
        /// any details of searched for users if the user has entered 
        /// a login name and clicked search.
        /// </returns>
        public ActionResult Index(string loginname)
        {
            IUser user = new User();

            if (!String.IsNullOrWhiteSpace(loginname))
            {
                Regex rx = new Regex("^[a-zA-Z0-9-.]{1,39}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if(!rx.IsMatch(loginname))
                {
                    ModelState.AddModelError("Login", "Username may only contain alphanumeric characters or hyphens and cannot be longer than 39 characters");
                    return View(user);
                }

                IWebClient webClient = new WebClient();
                IRepository repository = new GithubRepository(webClient);
                ISearch search = new Search(repository);
                user = search.SearchUsers(loginname);
            }
            return View(user);
        }
    }
}