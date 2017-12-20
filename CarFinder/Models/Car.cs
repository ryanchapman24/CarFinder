using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarFinder.Models
{
    public class Car
    {
        public int Id { get; set; }
        public Nullable<int> MakeId { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public string Year { get; set; }
        public Nullable<int> BodyStyleId { get; set; }
        public Nullable<int> EnginePositionId { get; set; }
        public string EngineCC { get; set; }
        public string EngineCyl { get; set; }
        public Nullable<int> EngineTypeId { get; set; }
        public string EngineValvesPerCyl { get; set; }
        public string EnginePowerPS { get; set; }
        public string EnginePowerRPM { get; set; }
        public string EngineTorqueNM { get; set; }
        public string EngineTorqueRPM { get; set; }
        public string EngineBoreMM { get; set; }
        public string EngineStrokeMM { get; set; }
        public string EngineCompression { get; set; }
        public Nullable<int> EngineFuelId { get; set; }
        public string TopSpeedKPH { get; set; }
        public string ZeroTo100KPH { get; set; }
        public Nullable<int> DriveTypeId { get; set; }
        public Nullable<int> TransmissionTypeId { get; set; }
        public string Seats { get; set; }
        public string Doors { get; set; }
        public string WeightKG { get; set; }
        public string LengthMM { get; set; }
        public string WidthMM { get; set; }
        public string HeightMM { get; set; }
        public string WheelbaseMM { get; set; }
        public string LKM_Hwy { get; set; }
        public string LKM_Mixed { get; set; }
        public string LKM_City { get; set; }
        public string FuelCapacityL { get; set; }
        public string SoldInUS { get; set; }
        public string CO2 { get; set; }
        public string MakeDisplay { get; set; }

        public virtual BodyStyles BodyStyle { get; set; }
        public virtual DriveTypes DriveType { get; set; }
        public virtual EngineFuel EngineFuel { get; set; }
        public virtual EnginePositions EnginePosition { get; set; }
        public virtual EngineTypes EngineType { get; set; }
        public virtual Makes Make { get; set; }
        public virtual TransmissionTypes TransmissionType { get; set; }
    }

    public class carViewModel
    {
        //public Car Car { get; set; }
        public List<Result> RecallResults { get; set; }
        //public IEnumerable<ImageResults> ResultImage { get; set; }
        public IEnumerable<string> ImageUrl { get; set; }
    }
}