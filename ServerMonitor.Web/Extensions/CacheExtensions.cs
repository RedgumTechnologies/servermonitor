using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace ServerMonitor.Web
{
    public static class CacheExtensions
    {
        public static T SetAndGetItemFromCache<T>(this Cache cache, String cacheKeyAndLockHandle, Func<Cache, T> addItemToCache)
        {
            return SetAndGetItemFromCache(cache, cacheKeyAndLockHandle, cacheKeyAndLockHandle, addItemToCache);
        }
        public static T SetAndGetItemFromCache<T>(this Cache cache, String cacheKey, Object lockHandle, Func<Cache, T> addItemToCache)
        {
            Object item = cache.Get(cacheKey);

            if (item == null)
            {
                lock (lockHandle)
                {
                    item = cache.Get(cacheKey);

                    if (item == null)
                    {
                        item = addItemToCache(cache);
                    }
                    else
                    {
                        //The sought item has been added meanwhile in another thread/process
                    }
                }
            }
            else
            {
                //Cache contains the key, return it
            }

            if (item is T)
            {
                return (T)item;
            }
            else
            {
                //The Object in the cache is not of type T, some other component might be using that key
                //Remove it from cache first!
                cache.Remove(cacheKey);
                throw new ApplicationException(String.Format("Object retrieved from the cache is not of the expected {0} type. Another component might be using the same key to store in the cache.", typeof(T).FullName));
            }
        }

        public static void Remove(this Cache cache, String cacheKey, Object lockHandle)
        {
            lock (lockHandle)
            {
                cache.Remove(cacheKey);
            }
        }
    }
}