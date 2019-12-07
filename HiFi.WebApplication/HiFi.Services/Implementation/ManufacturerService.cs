using HiFi.Data.Models;
using HiFi.Repository;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiFi.Services.Implementation
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IRepository<Manufacturer> _manufacturerRepository;
        private readonly IMemoryCache _memoryCache;

        public ManufacturerService(IMemoryCache memoryCache,IRepository<Manufacturer> manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Manufacturer>> GetAllManufacturers()
        {
            var data =await _manufacturerRepository.GetAll();
            return data;
        }

        public async Task<Manufacturer> GetManufacturerById(int manufacturerId)
        {
            return await _manufacturerRepository.GetById(manufacturerId);
        }
        
        public async Task<Manufacturer> GetManufacturerByIdAsync(int manufacturerId)
        {
            var result = await _manufacturerRepository.GetById(manufacturerId);
            return result;
        }

        /// <summary>
        /// Gets Vendor by itemid.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<Manufacturer> GetManufacturerByProductId(int productId)
        {
            return await _manufacturerRepository.GetManufacturerByProductId(productId);
        }

        public async Task<Manufacturer> InsertManufacturer(Manufacturer manufacturer)
        {
            //_manufacturerRepository.Insert(manufacturer);
            //return true;
            return await _manufacturerRepository.InsertData(manufacturer); ;
        }

        public async Task<bool> UpdateManufacturer(Manufacturer manufacturer)
        {
            return await _manufacturerRepository.Update(manufacturer);
        }
        public async Task<bool> DeleteManufacturer(Manufacturer manufacturer)
        {
            return await _manufacturerRepository.Delete(manufacturer);
        }
    }
}
