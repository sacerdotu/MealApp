using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealApp.Helpers
{
    public class WebApiLinks
    {
        public const string BaseIPAddress = "http://192.168.192.201";
        public const string ItemsFromToday =    "{0}/MealAppWebApi/api/providermenuitem/today";
        public const string ItemFromTodayById = "{0}/MealAppWebApi/api/providermenuitem/{1}";
        public const string LikeItemById =      "{0}/MealAppWebApi/api/providermenuitem/like/{1}";
        public const string DisLikeItemById =   "{0}/MealAppWebApi/api/providermenuitem/dislike/{1}";
    }
}
