using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    /// <summary>
    /// Класс-менеджер. Инициализирует классы-помощники
    /// </summary>
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;
        protected string testFundistUrl;

        protected LoginHelper loginHelper;
        protected NavigationHelper navigator;
        protected GroupHelper groupHelper;
        protected ContactHelper contactHelper;
        protected FundistNavigationHelper fundistNavigator;
        protected FundistLoginHelper fundistLogin;
        protected FundistFilterElementHelper fundistFilter;
        protected UserSummaryHelper fundistUserHelper;

        private static ThreadLocal<ApplicationManager> appManager = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost";
            testFundistUrl = "https://test.fundist.org";

            loginHelper = new LoginHelper(this);
            fundistNavigator = new FundistNavigationHelper(this, testFundistUrl);
            fundistLogin = new FundistLoginHelper(this);
            fundistFilter = new FundistFilterElementHelper(this);
            fundistUserHelper = new UserSummaryHelper(this);
            navigator = new NavigationHelper(this, baseURL);
            groupHelper = new GroupHelper(this);
            contactHelper = new ContactHelper(this);
        }

        ~ApplicationManager()
        {
            driver.Quit();
        }

        public static ApplicationManager GetInstance()
        {
            if (! appManager.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Navigator.GoToHomePage();
                appManager.Value = newInstance;
            }
            return appManager.Value;
        }

        public static ApplicationManager GetFundistInstance()
        {
            if (! appManager.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.FundistNavigator.GoToMainPage();
                appManager.Value = newInstance;
            }
            return appManager.Value;
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }


        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }

        public NavigationHelper Navigator
        {
            get
            {
                return navigator;
            }
        }

        public GroupHelper Groups
        {
            get
            {
                return groupHelper;
            }
        }

        public ContactHelper Contacts
        {
            get
            {
                return contactHelper;
            }
        }

        public FundistNavigationHelper FundistNavigator
        {
            get
            {
                return fundistNavigator;
            }
        }

        public FundistLoginHelper FundistAuth
        {
            get
            {
                return fundistLogin;
            }
        }

        public UserSummaryHelper UserHelper
        {
            get
            {
                return fundistUserHelper;
            }
        }

        public FundistFilterElementHelper FundistFilter
        {
            get
            {
                return fundistFilter;
            }
        }
    }
}
