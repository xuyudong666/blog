using AutoMapper;
using SwiftCode.BBS.Model.Models;
using SwiftCode.BBS.Model.ViewModels.UserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCode.BBS.Extensions.AutoMapper
{
    public class UserInfoProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public UserInfoProfile()
        {
            CreateMap<CreateUserInfoInputDto, UserInfo>();
            CreateMap<UserInfo, UserInfoDto>();

            CreateMap<UserInfo, UserInfoDetailsDto>();

        }
    }
}
