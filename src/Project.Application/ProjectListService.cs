using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Project.Application.DTOs;
using Project.Application.Services;
using Project.Domain;
using Project.Domain.Enum;
using Project.Persistence.IPersistence;

namespace Project.Application
{
    public class ProjectListService : IProjectListService
    {
        public readonly IGeralPersistence _geralPersistence;
        public readonly IProjectListPersistence _projectListPersistence;
        public readonly IMapper _mapper;
        public ProjectListService(IGeralPersistence geralPersistence,
                                  IProjectListPersistence projectListPersistence,
                                  IMapper mapper)
        {
            _projectListPersistence = projectListPersistence;
            _geralPersistence = geralPersistence;
            _mapper = mapper;
        }
        public async Task<ProjectListDTO> AddProject(int userId, ProjectListDTO model)
        {
            try
            {
                var project = _mapper.Map<ProjectList>(model);
                project.UserId = userId;
                var projectUser = new ProjectUser();

                _geralPersistence.Add(project);
                if (await _geralPersistence.SaveChangesAsync())
                {
                    projectUser.UserId = userId;
                    projectUser.ProjectId = project.Id;
                    projectUser.RoleId = (int)RoleEnum.Admin;

                    _geralPersistence.Add(projectUser);
                    if (await _geralPersistence.SaveChangesAsync())
                    {
                        if (model.ProjectUsers != null)
                        {
                            foreach(var user in model.ProjectUsers)
                            {
                                await AddUserByProject(project.Id, user);
                            }
                        }
                        var result = await _projectListPersistence.GetProjectByIdAsync(userId, project.Id);
                        return _mapper.Map<ProjectListDTO>(result);
                    }

                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProjectListDTO> UpdateProject(int userId, int projectId, ProjectListDTO model)
        {
            try
            {
                var project = await _projectListPersistence.GetProjectByIdAsync(userId, projectId);
                if (project == null) return null;

                model.Id = projectId;
                model.UserId = userId;

                _mapper.Map(model, project);

                _geralPersistence.Update(project);
                if (await _geralPersistence.SaveChangesAsync())
                {
                    var result = await _projectListPersistence.GetProjectByIdAsync(userId, project.Id);
                    return _mapper.Map<ProjectListDTO>(result);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ProjectUserDTO[]> SaveUsersByProject(int projectId, IEnumerable<ProjectUserDTO> models)
        {
            try
            {
                var usersList = await _projectListPersistence.GetUsersByProjectByIdAsync(projectId);
                if (usersList == null) return null;

                foreach(var model in models)
                {
                    var userInList = usersList.FirstOrDefault(u => u.UserId == model.UserId);
                    if (userInList == null)
                    {
                        await AddUserByProject(projectId, model);
                    }
                    else if (model.Order == -1)
                    {
                        await DeleteUserByProject(projectId, model.UserId);
                    }
                    else {
                        var user = usersList.FirstOrDefault(user => user.UserId == model.UserId);
                        model.ProjectId = projectId;

                        _mapper.Map(model, user);

                        _geralPersistence.Update<ProjectUser>(user);
                        await _geralPersistence.SaveChangesAsync();
                    }
                }

                var result = await _projectListPersistence.GetUsersByProjectByIdAsync(projectId);
                return _mapper.Map<ProjectUserDTO[]>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task AddUserByProject(int projetcId, ProjectUserDTO model)
        {
            try
            {
                var user = _mapper.Map<ProjectUser>(model);
                user.ProjectId = projetcId;

                _geralPersistence.Add(user);

                await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> DeleteUserByProject(int projectId, int userId)
        {
            try
            {
                var user = await _projectListPersistence.GetUserByIdByProjectAsync(projectId, userId);
                if (user == null) throw new Exception("Don't found Id");

                _geralPersistence.Delete<ProjectUser>(user);
                return await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteProject(int userId,int projectId)
        {
            try
            {
                var project = await _projectListPersistence.GetProjectByIdAsync(userId, projectId);
                if (project == null) throw new Exception("Don't found Id");

                _geralPersistence.Delete<ProjectList>(project);
                return await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProjectListDTO[]> GetAllProjectsAsync(int userId, bool includeUsers = false, bool includeTags = true, bool includeLists = false)
        {
            try
            {
                var projects = await _projectListPersistence.GetAllProjectsAsync(userId);
                if (projects == null) return null;

                var result = _mapper.Map<ProjectListDTO[]>(projects);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProjectListDTO[]> GetAllProjectsByUserAsync(int userId, string user, bool includeUsers = false, bool includeTags = false, bool includeLists = false)
        {
            try
            {
                var projects = await _projectListPersistence.GetAllProjectsByUserAsync(userId, user, includeUsers, includeTags, includeLists);
                if (projects == null) return null;

                var result = _mapper.Map<ProjectListDTO[]>(projects);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProjectListDTO> GetProjectByIdAsync(int userId, int ProjectListId)
        {
            try
            {
                var project = await _projectListPersistence.GetProjectByIdAsync(userId, ProjectListId);
                if (project == null) return null;

                var result = _mapper.Map<ProjectListDTO>(project);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}