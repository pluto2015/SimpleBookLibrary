using AutoMapper;
using SimpleBookLibrary.Convertor;
using SimpleBookLibrary.Data;
using SimpleBookLibrary.Model;

namespace SimpleBookLibrary
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<BaseEntity,BaseModel>()
                .ForMember(x=>x.Created,opt=>opt.ConvertUsing(new TimestampToDateTime(),y=>y.Created))
                .ForMember(x=>x.Updated,opt=>opt.ConvertUsing(new TimestampToDateTimeNull(),y=>y.Updated));
            CreateMap<BaseModel,BaseEntity>()
                .ForMember(x => x.Created, opt => opt.ConvertUsing(new DateTimeToTimestamp(),y=>y.Created))
                .ForMember(x => x.Updated,opt => opt.ConvertUsing(new DateTimeToTimestampNull(),y=>y.Updated));

            CreateMap<BookEntity,BookModel>()
                .ForMember(x=>x.PurchaseDateTime,opt=>opt.ConvertUsing(new TimestampToDateTimeNull(),y=> y.PurchaseDateTime));
            CreateMap<BookModel,BookEntity>()
                .ForMember(x => x.PurchaseDateTime, opt => opt.ConvertUsing(new DateTimeToTimestampNull(), y => y.PurchaseDateTime));

            CreateMap<BorrowerEntity, BorrowerModel>();
            CreateMap<BorrowerModel, BorrowerEntity>();

            CreateMap<BorrowHistoryEntity, BorrowHistoryModel>()
                .ForMember(x => x.BorrowDateTime, opt => opt.ConvertUsing(new TimestampToDateTime(), y => y.BorrowDateTime))
                .ForMember(x => x.ReturnDateTime, opt => opt.ConvertUsing(new TimestampToDateTimeNull(), y => y.ReturnDateTime));
            CreateMap<BorrowHistoryModel, BorrowHistoryEntity>()
                .ForMember(x => x.BorrowDateTime, opt => opt.ConvertUsing(new DateTimeToTimestamp(), y => y.BorrowDateTime))
                .ForMember(x => x.ReturnDateTime, opt => opt.ConvertUsing(new DateTimeToTimestampNull(), y => y.ReturnDateTime));

            CreateMap<DepartmentEntity, DepartmentModel>();
            CreateMap<DepartmentModel, DepartmentEntity>();
        }
    }
}
