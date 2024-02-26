namespace BreweryAPI.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Beer, BeerRequest>().ReverseMap();
        CreateMap<WholesalerBuyOrderRequest, Order>()
            .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderedBeers));
        CreateMap<OrderedBeerRequest, OrderDetail>();

        CreateMap<Beer, BeerResponse>().ReverseMap();
        CreateMap<Order, OrderResponse>();
        CreateMap<OrderDetail, OrderDetailResponse>();

        CreateMap<InventoryItem, InventoryItemResponse>();
    }
}
