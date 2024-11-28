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
                .IncludeBase<BaseEntity,BaseModel>()
                .ForMember(x=>x.PurchaseDateTime,opt=>opt.ConvertUsing(new TimestampToDateTimeNull(),y=> y.PurchaseDateTime))
                .ForMember(x=>x.Department,opt=>opt.MapFrom(y=>(y.Department == null? null:y.Department.Name)));
            CreateMap<BookModel,BookEntity>()
                .IncludeBase<BaseModel,BaseEntity>()
                .ForMember(x => x.PurchaseDateTime, opt => opt.ConvertUsing(new DateTimeToTimestampNull(), y => y.PurchaseDateTime))
                .ForMember(x=>x.Department,opt=>opt.Ignore());

            CreateMap<BorrowerEntity, BorrowerModel>()
                .IncludeBase<BaseEntity, BaseModel>();
            CreateMap<BorrowerModel, BorrowerEntity>()
                .IncludeBase<BaseModel, BaseEntity>();

            CreateMap<BorrowHistoryEntity, BorrowHistoryModel>()
                .IncludeBase<BaseEntity, BaseModel>()
                .ForMember(x => x.BorrowDateTime, opt => opt.ConvertUsing(new TimestampToDateTime(), y => y.BorrowDateTime))
                .ForMember(x => x.ReturnDateTime, opt => opt.ConvertUsing(new TimestampToDateTimeNull(), y => y.ReturnDateTime))
                .ForMember(x=>x.Borrower,opt=>opt.MapFrom(y=>(y.Borrower == null ?null:y.Borrower.Name)));
            CreateMap<BorrowHistoryModel, BorrowHistoryEntity>()
                .IncludeBase<BaseModel, BaseEntity>()
                .ForMember(x => x.BorrowDateTime, opt => opt.ConvertUsing(new DateTimeToTimestamp(), y => y.BorrowDateTime))
                .ForMember(x => x.ReturnDateTime, opt => opt.ConvertUsing(new DateTimeToTimestampNull(), y => y.ReturnDateTime))
                .ForMember(x => x.Borrower, opt => opt.Ignore());

            CreateMap<DepartmentEntity, DepartmentModel>()
                .IncludeBase<BaseEntity, BaseModel>();
            CreateMap<DepartmentModel, DepartmentEntity>()
                .IncludeBase<BaseModel, BaseEntity>();
        }
    }
}
