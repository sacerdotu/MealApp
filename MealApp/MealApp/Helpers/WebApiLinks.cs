using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealApp.Helpers
{
    public class WebApiLinks
    {
        
        public const string ItemsFromToday = "http://192.168.0.103:8765/api/providermenuitem/today";
        public const string ItemFromTodayById = "http://192.168.0.103:8765/api/providermenuitem/{0}";
        public const string LikeItemById = "http://192.168.0.103:8765/api/providermenuitem/like/{0}";
        public const string DisLikeItemById = "http://192.168.0.103:8765/api/providermenuitem/dislike/{0}";
    }
}
