﻿using CourseProject.Models;
using Microsoft.AspNetCore.Components;

namespace CourseProject.Services
{
    public class ViewService
    {
        public ReviewService _reviewService;
        public UserService _userService;

        public List<Review>? Reviews { get; private set; }
        public List<Review>? CurrentUserReviews { get; private set; }
        public List<User>? Users { get; private set; }

        public ViewService(ReviewService reviewService, UserService userService)
        {
            _reviewService = reviewService;
            _userService = userService;
        }

        public async Task GetReviewsAsync()
        {
            Reviews = await _reviewService.GetReviewsAsync();
            NotifyListChanged(Reviews, EventArgs.Empty);
        }

        public async Task GetUserReviewsAsync()
        {
            CurrentUserReviews = await _reviewService.GetCurrentUserReviewsAsync(await _userService.GetCurrentUserId());
            NotifyListChanged(CurrentUserReviews, EventArgs.Empty);
        }

        public async Task GetUsersAsync()
        {
            Users = await _userService.GetAllUsersAsync();
            NotifyListChanged(Users, EventArgs.Empty);
        }

        public async Task DeleteReviewAsync(Review review)
        {
            await _reviewService.DeleteReview(review);
            await GetReviewsAsync();
            await GetUserReviewsAsync();
        }
        
        public event EventHandler<EventArgs>? ListChanged;

        public void NotifyListChanged(object sender, EventArgs e)
            => ListChanged?.Invoke(sender, e);
    }
}