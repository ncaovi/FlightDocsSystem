using FlightDocsSystem.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightDocsSystem.Interface
{
    public interface ICargoManifest
    {
        public Task<List<CargoManifest>> GetCargoManifestAllAsync();
        public Task<int> EditCargoManifestAsync(int id, CargoManifest CargoManifests);
        public Task<int> AddCargoManifestAsync(CargoManifest CargoManifests);
        public Task<CargoManifest> GetCargoManifestAsync(int? id);
    }
}
