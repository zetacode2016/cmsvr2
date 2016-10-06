using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Channels;
using CMS2.Common.Constants;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class EmployeePositionMappingBL : BaseAPCargoBL<EmployeePositionMapping>
    {
        private ICmsUoW _unitOfWork;
        private AreaBL areaService;
        private BranchSatOfficeBL bsoService;
        private GatewaySatOfficeBL gatewaySatService;
        private BranchCorpOfficeBL bcoService;

        public EmployeePositionMappingBL()
        {
            _unitOfWork = GetUnitOfWork();
            areaService = new AreaBL(_unitOfWork);
            bsoService = new BranchSatOfficeBL(_unitOfWork);
            gatewaySatService = new GatewaySatOfficeBL(_unitOfWork);
            bcoService = new BranchCorpOfficeBL(_unitOfWork);
        }

        public EmployeePositionMappingBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
            areaService = new AreaBL(unitOfWork);
            bsoService = new BranchSatOfficeBL(unitOfWork);
            gatewaySatService = new GatewaySatOfficeBL(unitOfWork);
            bcoService = new BranchCorpOfficeBL(unitOfWork);
        }

        public override Expression<Func<EmployeePositionMapping, object>>[] Includes()
        {
            return new Expression<Func<EmployeePositionMapping, object>>[]
                {
                    x=>x.Position,
                        x=>x.Employee,
                        x=>x.Department
                };
        }

        public override List<EmployeePositionMapping> FilterActive()
        {
            var mappings = base.FilterActive();
            foreach (var item in mappings)
            {
                switch (item.LocationAssignment)
                {
                    case AssignLocationConstant.Area:
                        item.AssignedLocation = areaService.GetById(item.AssignedLocationId);
                        break;
                    case AssignLocationConstant.BSO:
                        item.AssignedLocation = bsoService.GetById(item.AssignedLocationId);
                        break;
                    case AssignLocationConstant.GatewaySat:
                        item.AssignedLocation = gatewaySatService.GetById(item.AssignedLocationId);
                        break;
                    case AssignLocationConstant.BCO:
                        item.AssignedLocation = bcoService.GetById(item.AssignedLocationId);
                        break;
                }
            }
            return mappings;
        }

        public override List<EmployeePositionMapping> FilterActiveBy(Expression<Func<EmployeePositionMapping, bool>> filter)
        {
            var mappings = base.FilterActiveBy(filter);
            foreach (var item in mappings)
            {
                switch (item.LocationAssignment)
                {
                    case AssignLocationConstant.Area:
                        item.AssignedLocation = areaService.GetById(item.AssignedLocationId);
                        break;
                    case AssignLocationConstant.BSO:
                        item.AssignedLocation = bsoService.GetById(item.AssignedLocationId);
                        break;
                    case AssignLocationConstant.GatewaySat:
                        item.AssignedLocation = gatewaySatService.GetById(item.AssignedLocationId);
                        break;
                    case AssignLocationConstant.BCO:
                        item.AssignedLocation = bcoService.GetById(item.AssignedLocationId);
                        break;
                }
            }
            return mappings;
        }

        /// <summary>
        /// Get the Assignment of an Employee covered by the specified date
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public EmployeePositionMapping GetByEmployeeDate(Guid employeeId, DateTime date)
        {
            DateTime _date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            var models = FilterBy(x => x.EmployeeId == employeeId).OrderByDescending(x => x.DateAssigned).ToList();
            if (models != null)
            {
                foreach (var item in models)
                {
                    if (_date >= item.DateAssigned)
                    {
                        switch (item.LocationAssignment)
                        {
                            case AssignLocationConstant.Area:
                                item.AssignedLocation = areaService.GetById(item.AssignedLocationId);
                                break;
                            case AssignLocationConstant.BSO:
                                item.AssignedLocation = bsoService.GetById(item.AssignedLocationId);
                                break;
                            case AssignLocationConstant.GatewaySat:
                                item.AssignedLocation = gatewaySatService.GetById(item.AssignedLocationId);
                                break;
                            default:
                                item.AssignedLocation = bcoService.GetById(item.AssignedLocationId);
                                break;
                        }
                        return item;
                    }
                }
            }
            return null;
        }
        public EmployeePositionMapping GetByEmployeeDatePosition(Guid employeeId, DateTime date,string position)
        {
            DateTime _date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            var models = FilterBy(x => x.EmployeeId == employeeId && x.Position.PositionName.Equals(position)).OrderByDescending(x => x.DateAssigned).ToList();
            if (models != null)
            {
                foreach (var item in models)
                {
                    if (_date >= item.DateAssigned)
                    {
                        switch (item.LocationAssignment)
                        {
                            case AssignLocationConstant.Area:
                                item.AssignedLocation = areaService.GetById(item.AssignedLocationId);
                                break;
                            case AssignLocationConstant.BSO:
                                item.AssignedLocation = bsoService.GetById(item.AssignedLocationId);
                                break;
                            case AssignLocationConstant.GatewaySat:
                                item.AssignedLocation = gatewaySatService.GetById(item.AssignedLocationId);
                                break;
                            default:
                                item.AssignedLocation = bcoService.GetById(item.AssignedLocationId);
                                break;
                        }
                        return item;
                    }
                }
            }
            return null;
        }

        public EmployeePositionMapping GetByEmployeeDateCityName(Guid employeeId, DateTime date, string cityName)
        {
            DateTime _date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            var models = FilterBy(x => x.EmployeeId == employeeId && !x.LocationAssignment.Equals(AssignLocationConstant.BCO)).OrderByDescending(x => x.DateAssigned).ToList();
            if (models != null)
            {
                foreach (var item in models)
                {
                    var revenueunit = (RevenueUnit)item.AssignedLocation;
                    if (revenueunit.City.CityName.Equals(cityName))
                    {
                        if (_date >= item.DateAssigned)
                        {
                            return item;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the Assignments covered by the date provided
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<EmployeePositionMapping> GetByDate(DateTime date)
        {
            DateTime _date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            List<EmployeePositionMapping> models = new List<EmployeePositionMapping>();
            var _models = GetAll().OrderByDescending(x => x.DateAssigned).ToList();
            if (_models != null)
            {
                foreach (var item in _models)
                {
                    if (_date >= item.DateAssigned)
                    {
                        switch (item.LocationAssignment)
                        {
                            case AssignLocationConstant.Area:
                                item.AssignedLocation = areaService.GetById(item.AssignedLocationId);
                                break;
                            case AssignLocationConstant.BSO:
                                item.AssignedLocation = bsoService.GetById(item.AssignedLocationId);
                                break;
                            case AssignLocationConstant.GatewaySat:
                                item.AssignedLocation = gatewaySatService.GetById(item.AssignedLocationId);
                                break;
                            default:
                                item.AssignedLocation = bcoService.GetById(item.AssignedLocationId);
                                break;
                        }
                        models.Add(item);
                    }
                }
            }
            return models;
        }

        public List<EmployeePositionMapping> GetByDatePosition(DateTime date, string position)
        {
            DateTime _date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            List<EmployeePositionMapping> models = new List<EmployeePositionMapping>();
            var _models = FilterBy(x => x.Position.PositionName.Equals(position)).OrderByDescending(x => x.DateAssigned).ToList();
            if (_models != null)
            {
                foreach (var item in _models)
                {
                    if (_date >= item.DateAssigned)
                    {
                        switch (item.LocationAssignment)
                        {
                            case AssignLocationConstant.Area:
                                item.AssignedLocation = areaService.GetById(item.AssignedLocationId);
                                break;
                            case AssignLocationConstant.BSO:
                                item.AssignedLocation = bsoService.GetById(item.AssignedLocationId);
                                break;
                            case AssignLocationConstant.GatewaySat:
                                item.AssignedLocation = gatewaySatService.GetById(item.AssignedLocationId);
                                break;
                            default:
                                item.AssignedLocation = bcoService.GetById(item.AssignedLocationId);
                                break;
                        }
                        models.Add(item);
                    }
                }
            }
            return models;
        }

        public List<EmployeePositionMapping> GetByDateBco(DateTime date, Guid bcoId)
        {
            DateTime _date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            List<EmployeePositionMapping> models = new List<EmployeePositionMapping>();
            var _models = base.FilterBy(x => !x.LocationAssignment.Equals(AssignLocationConstant.BCO)).OrderByDescending(x => x.DateAssigned).ToList();
            if (_models != null)
            {
                foreach (var item in _models)
                {
                    switch (item.LocationAssignment)
                    {
                        case AssignLocationConstant.Area:
                            item.AssignedLocation = areaService.GetById(item.AssignedLocationId);
                            break;
                        case AssignLocationConstant.BSO:
                            item.AssignedLocation = bsoService.GetById(item.AssignedLocationId);
                            break;
                        case AssignLocationConstant.GatewaySat:
                            item.AssignedLocation = gatewaySatService.GetById(item.AssignedLocationId);
                            break;
                    }

                    if (item.AssignedLocation.Cluster.BranchCorpOffice.BranchCorpOfficeId == bcoId)
                    {
                        if (_date >= item.DateAssigned)
                        {
                            models.Add(item);
                        }
                    }
                }
            }
            return models;
        }

        public List<EmployeePositionMapping> GetByDateBcoName(DateTime date, string bcoName)
        {
            DateTime _date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            List<EmployeePositionMapping> models = new List<EmployeePositionMapping>();
            var _models =
                base.FilterBy(x => !x.LocationAssignment.Equals(AssignLocationConstant.BCO))
                    .OrderByDescending(x => x.DateAssigned)
                    .ToList();
            if (_models != null)
            {
                foreach (var item in _models)
                {
                    switch (item.LocationAssignment)
                    {
                        case AssignLocationConstant.Area:
                            item.AssignedLocation = areaService.GetById(item.AssignedLocationId);
                            break;
                        case AssignLocationConstant.BSO:
                            item.AssignedLocation = bsoService.GetById(item.AssignedLocationId);
                            break;
                        case AssignLocationConstant.GatewaySat:
                            item.AssignedLocation = gatewaySatService.GetById(item.AssignedLocationId);
                            break;
                    }
                    if (item.AssignedLocation.Cluster.BranchCorpOffice.BranchCorpOfficeName.Equals(bcoName))
                    {
                        if (_date >= item.DateAssigned)
                        {
                            models.Add(item);
                        }
                    }
                }
            }
            return models;
        }

        public List<EmployeePositionMapping> GetByDateRevenuUnitName(DateTime date, string revenueUnitName)
        {
            DateTime _date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            List<EmployeePositionMapping> models = new List<EmployeePositionMapping>();
            var _models = base.FilterBy(x => !x.LocationAssignment.Equals(AssignLocationConstant.BCO)).OrderByDescending(x => x.DateAssigned).ToList();
            if (_models != null)
            {
                foreach (var item in _models)
                {
                    switch (item.LocationAssignment)
                    {
                        case AssignLocationConstant.Area:
                            item.AssignedLocation = areaService.GetById(item.AssignedLocationId);
                            break;
                        case AssignLocationConstant.BSO:
                            item.AssignedLocation = bsoService.GetById(item.AssignedLocationId);
                            break;
                        case AssignLocationConstant.GatewaySat:
                            item.AssignedLocation = gatewaySatService.GetById(item.AssignedLocationId);
                            break;
                    }
                    if (item.AssignedLocation.RevenueUnitName.Equals(revenueUnitName))
                    {
                        if (_date >= item.DateAssigned)
                        {
                            models.Add(item);
                        }
                    }
                }
            }
            return models;
        }
    }
}
