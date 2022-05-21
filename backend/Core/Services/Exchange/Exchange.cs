using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Exchange.WebServices.Data;
using System;

using bitcomTickets.Data;
using bitcomTickets.Core.Types;

namespace bitcomTickets.Core.Services
{
    public partial class Exchange
    {
        
        public Exchange(IHttpContextAccessor httpContextAccessor, IApplicationUserRepository userRepository, IWebHostEnvironment environment)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _environment = environment;
            InitExchange();
        }

        private IHttpContextAccessor _httpContextAccessor;
        private IApplicationUserRepository _userRepository;
        private IWebHostEnvironment _environment;
        private UserConfig _userConfig;

        public ExchangeService Service { get; private set; }

        private void InitExchange()
        {
            _userConfig = _userRepository.GetUserConfig();
            Service = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
            //TODO: przenieść adress do konfiguracji!
            Service.Url = new Uri("https://poczta.bitcom.com.pl/ews/exchange.asmx");
            Service.Credentials = _userConfig?.Credentials;
            
        }

        public ExchangeConnectionStatus GetServiceStatus()
        {
            ExchangeConnectionStatus status = new ExchangeConnectionStatus();
            status.IsUserConfig = _userConfig != null;
            try
            {
                ItemView view = new ItemView(1);
                FolderId mailbox = new FolderId(WellKnownFolderName.Inbox, "pomoc@bitcom.com.pl");
                FindItemsResults<Item> findResults = Service.FindItems(mailbox, view);
            }
            catch (ServiceLocalException)
            {
                status.IsUserConfig = false;
            }
            catch (ServiceRequestException e) 
            {
                status.IsUserConfig = _userConfig != null;
                status.IsUnauthorized = e.Message.Contains("401");
                status.ConnectionErrors = !status.IsUnauthorized;
            }
            
            return status;

        }


        ///ServiceLocalException brak ustawień

        ///ServiceRequestException




    }
}
