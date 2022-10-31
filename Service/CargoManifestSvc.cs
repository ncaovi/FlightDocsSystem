using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.Interface;
using FlightDocsSystem.Model;
using FlightDocsSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace FlightDocsSystem.Service
{
    public class CargoManifestSvc : ICargoManifest
    {
        protected DataContext _context;
        public CargoManifestSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<int> AddCargoManifestAsync(CargoManifest CargoManifests)
        {
            int ret = 0;
            try
            {
                await _context.AddAsync(CargoManifests);
                await _context.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                ret = 0;
            }
            return ret;
        }

        public async Task<int> EditCargoManifestAsync(int id, CargoManifest CargoManifests)
        {
            int ret = 0;
            try
            {
                CargoManifest cargo = null;
                cargo = await GetCargoManifestAsync(CargoManifests.FlightId);

                cargo.FlightNo = CargoManifests.FlightNo;
                cargo.PointOfLoading = CargoManifests.PointOfLoading;
                cargo.PointOfUnLoading = CargoManifests.PointOfUnLoading;

                _context.Update(cargo);
                await _context.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                ret = 0;
            }
            return ret;
        }

        public async Task<List<CargoManifest>> GetCargoManifestAllAsync()
        {
            var dataContext = _context.CargoManifests;
            return await dataContext.ToListAsync();
        }

        public async Task<CargoManifest> GetCargoManifestAsync(int? id)
        {
            var CargoManifests = await _context.CargoManifests
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (CargoManifests == null)
            {
                return null;
            }

            return CargoManifests;
        }

    }
}