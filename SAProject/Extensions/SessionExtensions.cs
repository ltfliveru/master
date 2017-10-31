using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAProject.Extensions
{
    public static class SessionExtensions
    {
        /// <summary> 
        /// Get value. 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="session"></param> 
        /// <param name="key"></param> 
        /// <returns></returns> 
        public static T GetData<T>(this HttpSessionStateBase session, string key)
        {
            return (T)session[key];
        }
        /// <summary> 
        /// Set value. 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="session"></param> 
        /// <param name="key"></param> 
        /// <param name="value"></param> 
        public static void SetData<T>(this HttpSessionStateBase session, string key, object value)
        {
            session[key] = value;
        }
    }
}