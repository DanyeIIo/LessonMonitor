﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LessonMonitor.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        public UsersController()
        {

        }

        [HttpGet]
        public User[] Get(string userName)
        {
            var rand = new Random();
            var users = new List<User>();
            for (int i = 0; i < 11; i++)
            {
                var user = new User();
                user.Age = rand.Next(1, 101);
                user.Name = userName + i;
                users.Add(user);
            }
            return users.ToArray();
        }
    }
}