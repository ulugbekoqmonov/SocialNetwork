using Application.Comments.CreateComment;
using Application.Posts.CreatePost;
using Application.Posts.GetAllPosts;
using Application.Posts.GetByIdPost;
using Application.Users.CreateUser;
using AutoMapper;
using Domain.Models.Entities;

namespace Application.Mappings;

public class MapProfiles:Profile
{
	public MapProfiles()
	{
		CreateMap<CreateCommentCommand,Comment>().ReverseMap();
		CreateMap<CreatePostCommand,Post>().ReverseMap();
		CreateMap<CreateUserCommand,User>().ReverseMap();
		CreateMap<GetAllPostsQuery, IQueryable<Post>>().ReverseMap();
		CreateMap<GetByIdPostQuery, Post>().ReverseMap();
	}
}
