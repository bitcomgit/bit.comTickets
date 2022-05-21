using Newtonsoft.Json;

namespace bitcomTickets.Core.Types
{ 
    public class ExchangeConnectionStatus
    {
        public bool IsUserConfig { get; set; }
        public bool IsUnauthorized { get; set; }
        public bool ConnectionErrors { get; set; }
        
    }
}
