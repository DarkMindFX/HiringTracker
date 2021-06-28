




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class UserConvertor
    {
        public static DTO.User Convert(Interfaces.Entities.User entity, IUrlHelper url)
        {
            var dto = new DTO.User()
            {
        		        ID = entity.ID,

				        Login = entity.Login,

				        FirstName = entity.FirstName,

				        LastName = entity.LastName,

				        Email = entity.Email,

				        Description = entity.Description,

				        PwdHash = entity.PwdHash,

				        Salt = entity.Salt,

				
            };

            
            dto.Links.Add(new DTO.Link(url.Action("GetUser", "users", new { id = dto.ID  }), "self", "GET"));
            dto.Links.Add(new DTO.Link(url.Action("DeleteUser", "users", new { id = dto.ID  }), "delete_user", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("InsertUser", "users"), "insert_user", "POST"));
            dto.Links.Add(new DTO.Link(url.Action("UpdateUser", "users"), "update_user", "PUT"));

            return dto;

        }

        public static Interfaces.Entities.User Convert(DTO.User dto)
        {
            var entity = new Interfaces.Entities.User()
            {
                
        		        ID = dto.ID,

				        Login = dto.Login,

				        FirstName = dto.FirstName,

				        LastName = dto.LastName,

				        Email = dto.Email,

				        Description = dto.Description,

				        PwdHash = dto.PwdHash,

				        Salt = dto.Salt,

				
     
            };

            return entity;
        }
    }
}
