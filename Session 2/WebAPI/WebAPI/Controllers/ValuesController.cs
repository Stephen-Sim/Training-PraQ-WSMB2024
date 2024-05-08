using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        public PraQTrainingSession1Entities ent { get; set; }

        public ValuesController()
        {
            ent = new PraQTrainingSession1Entities();
        }

        [HttpGet]
        public object GetUsers()
        {
            var users = ent.Users.Select(x => new
            {
                x.ID,
                x.Username,
                FullName = x.FirstName + " " + x.LastName,
                x.Age,
                Role = x.Role.Name
            });

            return Ok(users);
        }

        [HttpGet]
        public object Login(string username, string password)
        {
            var user = ent.Users.FirstOrDefault(x => x.Username == username && x.Password == password );

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(new
            {
                user.ID,
                user.Username,
                FullName = user.FirstName + " " + user.LastName,
                user.Age,
                Role = user.Role.Name
            });
        }

        [HttpGet]
        public object GetUser(int Id)
        {
            var user = ent.Users.Select(x => new
            {
                x.ID,
                x.Username,
                FullName = x.FirstName + " " + x.LastName,
                x.Age,
                Role = x.Role.Name
            }).FirstOrDefault(u => u.ID == Id);

            if (user == null)
            {
                return BadRequest("user is not found");
            }

            return Ok(user);
        }

        /*[HttpPost]
        public object StoreUser(UserDTO userDTO)
        {
            try
            {
                var user = new User
                {
                    Username = userDTO.Username,
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Age = userDTO.Age,
                    Gender = userDTO.Gender,
                    Password = userDTO.Password,
                    RoleID = userDTO.RoleID
                };

                ent.Users.Add(user);
                ent.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }*/

        [HttpPost]
        public object StoreUser(User user)
        {
            try
            {
                ent.Users.Add(user);
                ent.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public object UpdateUser(UserDTO userDTO)
        {
            try
            {
                var user = ent.Users.FirstOrDefault(x => x.ID == userDTO.ID);
                
                user.Username = userDTO.Username;
                user.Password = userDTO.Password;   
                user.LastName = userDTO.LastName;
                user.FirstName = userDTO.FirstName;
                user.Age = userDTO.Age;
                user.Gender = userDTO.Gender;   
                user.RoleID = userDTO.RoleID;

                ent.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public object DeleteUser (long Id)
        {
            try
            {
                var user = ent.Users.FirstOrDefault(x => x.ID == Id);
                
                ent.Users.Remove(user);
                ent.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public object GetRoles()
        {
            var roles = ent.Roles.ToList().Select(x => new
            {
                x.ID,
                x.Name
            });

            return Ok(roles);
        }

        public class UserDTO
        {
            public long ID { get; set; }
            public string Username { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; } 
            public string Password { get; set; } // hash
            public int Age { get; set; } // entry numeric value
            public bool Gender { get; set; }// radio button
            public long RoleID { get; set; } // combo box
        }
    }
}
