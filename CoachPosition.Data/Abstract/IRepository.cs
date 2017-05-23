using CoachPosition.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachPosition.Data.Abstract
{
    public interface IRepository
    {
        IQueryable<Train> Trains { get; }
        void SaveTrain(Train train);
        void DeleteTrain(Train train);
    }
}
