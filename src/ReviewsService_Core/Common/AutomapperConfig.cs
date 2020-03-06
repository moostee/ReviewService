﻿using AutoMapper;
using ReviewsService_Core.Domain.Entity;
using ReviewsService_Core.Domain.Form;
using ReviewsService_Core.Domain.Model;

namespace ReviewsService_Core.Common
{

    /// <summary>
    /// 
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static MapperConfiguration config;
        /// <summary>
        /// Registers the mappings.
        /// </summary>
        public static void RegisterMappings()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Client, ClientModel>().ReverseMap();
                cfg.CreateMap<Client, ClientForm>().ReverseMap();
                cfg.CreateMap<ClientModel, ClientForm>().ReverseMap();

                cfg.CreateMap<App, AppModel>().ReverseMap();
                cfg.CreateMap<App, AppForm>().ReverseMap();
                cfg.CreateMap<AppModel, AppForm>().ReverseMap();


                cfg.CreateMap<ReviewType, ReviewTypeModel>().ReverseMap();
                cfg.CreateMap<ReviewType, ReviewTypeForm>().ReverseMap();
                cfg.CreateMap<ReviewTypeModel, ReviewTypeForm>().ReverseMap();

                cfg.CreateMap<Review, ReviewModel>().ReverseMap();
                cfg.CreateMap<Review, ReviewForm>().ReverseMap();
                cfg.CreateMap<ReviewModel, ReviewForm>().ReverseMap();


                cfg.CreateMap<AppClient, AppClientModel>().ReverseMap();
                cfg.CreateMap<AppClient, AppClientForm>().ReverseMap();
                cfg.CreateMap<AppClientModel, AppClientForm>().ReverseMap();

                cfg.CreateMap<ReviewVoteType, ReviewVoteTypeModel>().ReverseMap();
                cfg.CreateMap<ReviewVoteType, ReviewVoteTypeForm>().ReverseMap();
                cfg.CreateMap<ReviewVoteTypeModel, ReviewVoteTypeForm>().ReverseMap();


                cfg.CreateMap<ReviewVote, ReviewVoteModel>().ReverseMap();
                cfg.CreateMap<ReviewVote, ReviewVoteForm>().ReverseMap();
                cfg.CreateMap<ReviewVoteModel, ReviewVoteForm>().ReverseMap();
            });
        }
    }
}
