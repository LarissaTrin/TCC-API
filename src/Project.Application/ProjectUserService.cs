using AutoMapper;
using Project.Application.DTOs;
using Project.Application.Services;
using Project.Domain;
using Project.Persistence.IPersistence;
using Project.Persistence.Models;

namespace Project.Application
{
    public class ProjectUserService : IProjectUserService
    {
        public readonly IGeralPersistence _geralPersistence;
        public readonly IProjectListPersistence _projectListPersistence;
        public readonly IProjectUserPersistence _projectUserPersistence;
        public readonly IMapper _mapper;
        public ProjectUserService(IGeralPersistence geralPersistence,
                                  IProjectListPersistence projectListPersistence,
                                  IProjectUserPersistence projectUserPersistence,
                                  IMapper mapper)
        {
            _projectUserPersistence = projectUserPersistence;
            _projectListPersistence = projectListPersistence;
            _geralPersistence = geralPersistence;
            _mapper = mapper;
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

        public async Task<ProjectUserDTO[]> SaveUsersByProject(int projectId, ProjectUserDTO[] models)
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
                        if (model.Order != -1) await AddUserByProject(projectId, model);
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

        public async Task<PageList<UserDTO>> GetAllUsersAsync(int userId, PageParams pageParams)
        {
            try
            {
                var projectUsers = await _projectUserPersistence.GetAllUsersAsync(userId, pageParams);
                if (projectUsers == null) return null;

                var result = _mapper.Map<PageList<UserDTO>>(projectUsers);

                result.CurrentPage = projectUsers.CurrentPage;
                result.TotalPages = projectUsers.TotalPages;
                result.PageSize = projectUsers.PageSize;
                result.TotalCount = projectUsers.TotalCount;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<UserDTO>> GetAllUsersByEditAsync(int projectId, PageParams pageParams)
        {
            try
            {
                var projectUsers = await _projectUserPersistence.GetAllUsersByEditAsync(projectId, pageParams);
                if (projectUsers == null) return null;

                var result = _mapper.Map<PageList<UserDTO>>(projectUsers);

                result.CurrentPage = projectUsers.CurrentPage;
                result.TotalPages = projectUsers.TotalPages;
                result.PageSize = projectUsers.PageSize;
                result.TotalCount = projectUsers.TotalCount;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}