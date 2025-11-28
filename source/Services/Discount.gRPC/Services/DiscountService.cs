namespace Discount.gRPC.Services
{
    public class DiscountService(DiscountContext db, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public async override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await db.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName)
                 ?? new() { ProductName = "No Discount", Amount = 0 };
            logger.LogInformation($"Discount For Product: {coupon.ProductName} Was Retrieved With Amount: {coupon.Amount}");
            return coupon.Adapt<CouponModel>();
        }

        public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            await db.AddAsync(coupon);
            await db.SaveChangesAsync();

            logger.LogInformation($"Dicount For product: {coupon.ProductName} was created with Amount: {coupon.Amount}");
            return coupon.Adapt<CouponModel>();
        }

        public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            db.Update(coupon);
            await db.SaveChangesAsync();

            logger.LogInformation($"Dicount For product: {coupon.ProductName} was Update with Amount: {coupon.Amount}");
            return coupon.Adapt<CouponModel>();
        }

        public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await db.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName)
                 ?? throw new RpcException(new Status(StatusCode.NotFound, $"Discount for product: {request.ProductName} Not Found"));
            db.Coupons.Remove(coupon);
            bool success = await db.SaveChangesAsync() > 0;
            logger.LogInformation($"Dicount For product: {coupon.ProductName} was Deleted successfully");
            return new() { Success = success };
        }
    }
}
