using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProViewGolf.Core.Dbo;
using ProViewGolf.Core.Dbo.Entities;
using ProViewGolf.Core.Dbo.Models;

namespace ProViewGolf.Core.Services
{
    public class EquipmentService
    {
        private readonly IProGolfContext _dbo;

        public EquipmentService(IProGolfContext context)
        {
            _dbo = context;
        }

        public List<Equipment> All(long studentId, EquipmentType type)
        {
            return _dbo.Equipments.Where(x => x.StudentId == studentId && x.Type == type).ToList();
        }

        public bool AddOrUpdate(EquipmentModel model, out string msg)
        {
            try
            {
                if (model.Type == EquipmentType.Irons ||
                    model.Type == EquipmentType.Woods)
                {
                    var equipments =
                        new List<string>(
                            model.IronWoodTypes.Split(',', StringSplitOptions.RemoveEmptyEntries));

                    foreach (var equipment in equipments)
                        if (model.EquipmentId == 0)
                        {
                            _dbo.Equipments.Add(new Equipment
                            {
                                Type = model.Type,
                                Club = (Club) int.Parse(equipment),
                                Brand = model.Brand,
                                Shaft = model.Shaft,
                                Model = model.Model,
                                ClubLoft = model.ClubLoft,
                                Grip = model.Grip,
                                Size = model.Size,
                                StudentId = model.StudentId
                            });
                        }
                        else
                        {
                            var eq = _dbo.Equipments.AsNoTracking()
                                .FirstOrDefault(x => x.EquipmentId == model.EquipmentId);
                            if (eq != null)
                            {
                                eq.Type = model.Type;
                                eq.Club = (Club) int.Parse(equipment);
                                eq.Brand = model.Brand;
                                eq.Shaft = model.Shaft;
                                eq.Model = model.Model;
                                eq.ClubLoft = model.ClubLoft;
                                eq.Grip = model.Grip;
                                eq.Size = model.Size;
                            }

                            _dbo.Equipments.Update(eq);
                        }
                }
                else
                {
                    if (model.EquipmentId == 0)
                    {
                        _dbo.Equipments.Add(new Equipment
                        {
                            Type = model.Type,
                            Club = model.Club,
                            Brand = model.Brand,
                            Shaft = model.Shaft,
                            Model = model.Model,
                            ClubLoft = model.ClubLoft,
                            Grip = model.Grip,
                            Size = model.Size,
                            StudentId = model.StudentId,
                            Pairs = model.Pairs,
                            Pieces = model.Pieces
                        });
                    }
                    else
                    {
                        var eq = _dbo.Equipments.AsNoTracking()
                            .FirstOrDefault(x => x.EquipmentId == model.EquipmentId);
                        if (eq != null)
                        {
                            eq.Type = model.Type;
                            eq.Club = model.Club;
                            eq.Brand = model.Brand;
                            eq.Shaft = model.Shaft;
                            eq.Model = model.Model;
                            eq.ClubLoft = model.ClubLoft;
                            eq.Grip = model.Grip;
                            eq.Size = model.Size;
                            eq.Pairs = model.Pairs;
                            eq.Pieces = model.Pieces;
                        }

                        _dbo.Equipments.Update(eq);
                    }
                }

                msg = "Equipment added / updated successfully";
                return _dbo.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public bool Delete(long id, out string msg)
        {
            try
            {
                if (_dbo.Equipments.Any(x => x.EquipmentId == id))
                {
                    //_dbo.Entry(_dbo.Equipments.Single(x => x.EquipmentId == id)).State =
                    //    EntityState.Deleted;

                    _dbo.Equipments.Remove(id);

                    _dbo.SaveChanges();
                    msg = "Equipment deleted";
                    return _dbo.SaveChanges() > 0;
                }

                msg = "No equipment found";
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

    }
}