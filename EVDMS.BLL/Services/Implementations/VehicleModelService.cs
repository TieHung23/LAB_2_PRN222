﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using EVDMS.DAL.Repositories.Abstractions;

namespace EVDMS.BLL.Services.Implementations
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IVehicleModelRepository _vehicleModelRepository;
        public VehicleModelService(IVehicleModelRepository vehicleModelRepository)
        {
            _vehicleModelRepository = vehicleModelRepository;
        }

        public async Task<IEnumerable<VehicleModel>> GetFeaturedModelsAsync(int count)
        {
            return await _vehicleModelRepository.GetFeaturedModelsAsync(count);
        }

        public async Task<IEnumerable<VehicleModel>> GetAllAsync(string searchTerm)
        {
            return await _vehicleModelRepository.GetAllAsync(searchTerm);
        }

        public async Task<VehicleModel> GetByIdAsync(Guid id)
        {
            return await _vehicleModelRepository.GetByIdAsync(id);
        }

        public async Task<VehicleModel> CreateAsync(VehicleModel vehicleModel)
        {
            return await _vehicleModelRepository.CreateAsync(vehicleModel);
        }

        public async Task UpdateAsync(VehicleModel vehicleModel)
        {
            await _vehicleModelRepository.UpdateAsync(vehicleModel);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _vehicleModelRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<VehicleModel>> SearchByNameAsync(string modelName)
        {
            return await _vehicleModelRepository.SearchByNameAsync(modelName);
        }
    }
}