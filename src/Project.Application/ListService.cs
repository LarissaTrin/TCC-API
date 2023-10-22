using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Project.Application.DTOs;
using Project.Application.Services;
using Project.Domain;
using Project.Persistence.IPersistence;

namespace Project.Application
{
    public class ListService : IListService
    {
        public readonly IGeralPersistence _geralPersistence;
        public readonly IListPersistence _listPersistence;
        public readonly IMapper _mapper;
        public ListService(IGeralPersistence geralPersistence,
                           IListPersistence listPersistence,
                           IMapper mapper)
        {
            _listPersistence = listPersistence;
            _geralPersistence = geralPersistence;
            _mapper = mapper;
        }
        public async Task AddList(int projetcId, ListDTO model)
        {
            try
            {
                var list = _mapper.Map<List>(model);
                list.ProjectId = projetcId;

                _geralPersistence.Add(list);

                await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ListDTO[]> SaveLists(int projectId, ListDTO[] models)
        {
            try
            {
                var lists = await _listPersistence.GetListsByProjectIdAsync(projectId);
                if (lists == null) return null;

                foreach(var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddList(projectId, model);
                    }
                    else if (model.Order == -1)
                    {
                        await DeleteList(projectId, model.Id);
                    }
                    else {
                        var list = lists.FirstOrDefault(list => list.Id == model.Id);
                        model.ProjectId = projectId;

                        _mapper.Map(model, list);

                        _geralPersistence.Update<List>(list);
                        await _geralPersistence.SaveChangesAsync();
                    }
                }

                var result = await _listPersistence.GetListsByProjectIdAsync(projectId);
                return _mapper.Map<ListDTO[]>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteList(int projectId, int listId)
        {
            try
            {
                var list = await _listPersistence.GetListByIdsAsync(projectId, listId);
                if (list == null) throw new Exception("Don't found Id");

                _geralPersistence.Delete<List>(list);
                return await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ListDTO[]> GetListsByProjectIdAsync(int projectId)
        {
            try
            {
                var lists = await _listPersistence.GetListsByProjectIdAsync(projectId);
                if (lists == null) return null;

                var result = _mapper.Map<ListDTO[]>(lists);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ListDTO> GetListByIdsAsync(int projectId, int listId)
        {
            try
            {
                var list = await _listPersistence.GetListByIdsAsync(projectId, listId);
                if (list == null) return null;

                var result = _mapper.Map<ListDTO>(list);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}