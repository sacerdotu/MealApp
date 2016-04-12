using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTryInno.Helpers
{
    public class WebApiLinks
    {
        public const string ItemsFromToday = "http://89.122.111.126:8765/api/providermenuitem/today";
        public const string ItemFromTodayById = "http://89.122.111.126:8765/api/providermenuitem/{0}";
        public const string LikeItemById = "http://89.122.111.126:8765/api/providermenuitem/like/{0}";
        public const string DisLikeItemById = "http://89.122.111.126:8765/api/providermenuitem/dislike/{0}";
    }
}
