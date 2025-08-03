using Microsoft.Extensions.Caching.Memory;

namespace WebApplication2.modle
{
    public interface IMemoryCacheService {

        Progress GetProgressByName(string name);
        void SaveProgress(Progress progress);
        bool DoesNameExist(string name);
        void DeleteValue(string name);
    }
    //how to create in memory cache
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _memoryCache;

        private const string CacheKey = "ProgressList"; //this is key used to store list of progress items in memory
        public MemoryCacheService(IMemoryCache memoryCache) => _memoryCache = memoryCache;

        // Remove a progress item by name
        public void DeleteValue(string name)
        {
            if (_memoryCache.TryGetValue(CacheKey, out List<Progress> list))//tries to get the list of progress items from memory
            {
                var item = list.FirstOrDefault(p => p.Name == name);//finds the first progress object in the list that maches the provided name
                if (item != null)//if item exists remove it from the list and save the updated list into the cache using the same key
                {
                    list.Remove(item);
                    _memoryCache.Set(CacheKey, list); 
                }
            }
        }

        // Check if an item with the given name exists
        public bool DoesNameExist(string name)
        {
            if (_memoryCache.TryGetValue(CacheKey, out List<Progress> list))//tries to retrive the list from cache
            {
                return list.Any(p => p.Name == name);//returns true if matches
            }
            return false;//returns false if it does not match
        }

        // get progress by name
        public Progress GetProgressByName(string name)
        {
            if(_memoryCache.TryGetValue(CacheKey, out List<Progress> list))//tries to retrive the list from cache
            {
                return list.FirstOrDefault(p => p.Name == name);//returns the first item that has the name
            }
            return null;//if not found returns null
        }

        // Save or replace a progress item in cache
        public void SaveProgress(Progress progress)
        {
            
            var list = _memoryCache.GetOrCreate(CacheKey, entry => new List<Progress>());//atempts to get the existing list,if it does not exists creates a new empty list and saves it

            
            var existing = list.FirstOrDefault(p => p.Name == progress.Name);//looks for existing progress with the same name
            if (existing != null)
            {
                list.Remove(existing);//if found removes it
            }

            list.Add(progress);//adds or updates the progress

            
            _memoryCache.Set(CacheKey, list);//saves the updated list

        }
    }
    public class FileMemoryService : IMemoryCacheService
    {
        public void DeleteValue(string name)
        {
            throw new NotImplementedException();
        }

        public bool DoesNameExist(string name)
        {
            throw new NotImplementedException();
        }

        public Progress GetProgressByName(string name)
        {
            throw new NotImplementedException();
        }

        public void SaveProgress(Progress progress)
        {
            throw new NotImplementedException();
        }

    }
    public class SQLMemoryService : IMemoryCacheService
    {
        public void DeleteValue(string name)
        {
            throw new NotImplementedException();
        }

        public bool DoesNameExist(string name)
        {
            throw new NotImplementedException();
        }

        public Progress GetProgressByName(string name)
        {
            throw new NotImplementedException();
        }

        public void SaveProgress(Progress progress)
        {
            throw new NotImplementedException();
        }


    }
}
