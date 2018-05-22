using AutoMapper;
using OnlineAuction.Core.Entities;
using OnlineAuction.Infrastructure.Identity;
using OnlineAuction.Web.ViewModels;
using OnlineAuction.Web.ViewModels.Lot;
using OnlineAuction.Web.ViewModels.Manage;
using System.Linq;

namespace OnlineAuction.Web.Mapping
{
    /// <summary>
    /// The AutoMapper class config.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to ViewModel.
            CreateMap<Lot, LotCreateViewModel>();

            CreateMap<Lot, LotSummaryViewModel>()
                .ForMember(lsvm => lsvm.Name, opt => opt.MapFrom(l => l.LotName))
                .ForMember(lsvm => lsvm.CurrentPrice, opt => opt.Ignore())
                .AfterMap((l, lsvm) =>
                {
                    if (!l.Bids.Any())
                        lsvm.CurrentPrice = l.StartPrice;
                    else
                        lsvm.CurrentPrice = l.Bids.Max(b => b.Amount);
                });

            CreateMap<Lot, LotDetailsViewModel>()
                .ForMember(ldvm => ldvm.AppUserID, opt => opt.Ignore())
                .ForMember(ldvm => ldvm.TotalBids, opt => opt.Ignore())
                .ForMember(ldvm => ldvm.Duration, opt => opt.Ignore())
                .ForMember(ldvm => ldvm.CurrentPrice, opt => opt.Ignore())
                .AfterMap((l, ldvm) =>
                {
                    if (!l.Bids.Any())
                        ldvm.CurrentPrice = l.StartPrice;
                    else
                        ldvm.CurrentPrice = l.Bids.Max(b => b.Amount);

                    //ldvm.AppUserID = l.AppUserID.Substring(0, 4) + "****";
                });

            CreateMap<Category, CategoryViewModel>();

            CreateMap<AppUser, ProfileSummaryViewModel>()
                .ForMember(pvm => pvm.UserID, opt => opt.MapFrom(au => au.Id));

            CreateMap<AppUser, ChangeProfileViewModel>()
                .ForMember(cpvm => cpvm.UserID, opt => opt.MapFrom(au => au.Id));

            // ViewModel to Domain.
            CreateMap<MakeBidViewModel, Bid>()
               .ForMember(b => b.AppUserID, opt => opt.Ignore())
               .ForMember(b => b.BidTime, opt => opt.Ignore());

            CreateMap<LotCreateViewModel, Lot>()
                .ForMember(l => l.LotID, opt => opt.Ignore());
        }
    }
}
