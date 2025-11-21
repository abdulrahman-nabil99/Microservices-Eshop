using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if (await session.Query<Models.Product>().AnyAsync(cancellation))
            {
                return;
            }
            session.Store<Models.Product>(GetSeedProducts());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Models.Product> GetSeedProducts()
        {
            IEnumerable<Models.Product> products = new List<Models.Product>
            {
                new Models.Product { Id = Guid.NewGuid(), Name = "Wireless Mouse", Categories = new List<string>{"Electronics", "Accessories"}, Description = "Ergonomic 2.4G wireless mouse.", ImageFile = "mouse1.jpg", Price = 199 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Gaming Keyboard", Categories = new List<string>{"Electronics", "Gaming"}, Description = "RGB mechanical gaming keyboard.", ImageFile = "keyboard1.jpg", Price = 750 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Bluetooth Speaker", Categories = new List<string>{"Electronics", "Audio"}, Description = "Portable Bluetooth speaker with deep bass.", ImageFile = "speaker1.jpg", Price = 320 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Laptop Stand", Categories = new List<string>{"Office", "Accessories"}, Description = "Adjustable aluminum laptop stand.", ImageFile = "stand1.jpg", Price = 180 },
                new Models.Product { Id = Guid.NewGuid(), Name = "USB-C Cable", Categories = new List<string>{"Electronics", "Cables"}, Description = "Fast-charging USB-C cable 1.5m.", ImageFile = "cable1.jpg", Price = 45 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Smart Watch", Categories = new List<string>{"Electronics", "Wearables"}, Description = "Fitness smart watch with heart rate sensor.", ImageFile = "watch1.jpg", Price = 599 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Leather Wallet", Categories = new List<string>{"Fashion", "Accessories"}, Description = "Premium leather wallet with multiple slots.", ImageFile = "wallet1.jpg", Price = 150 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Sunglasses", Categories = new List<string>{"Fashion"}, Description = "UV400 polarized sunglasses.", ImageFile = "sunglasses1.jpg", Price = 120 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Backpack", Categories = new List<string>{"Bags", "Travel"}, Description = "Waterproof travel backpack.", ImageFile = "backpack1.jpg", Price = 260 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Running Shoes", Categories = new List<string>{"Fashion", "Sports"}, Description = "Lightweight breathable running shoes.", ImageFile = "shoes1.jpg", Price = 420 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Desk Lamp", Categories = new List<string>{"Home", "Office"}, Description = "LED desk lamp with adjustable brightness.", ImageFile = "lamp1.jpg", Price = 95 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Water Bottle", Categories = new List<string>{"Sports", "Accessories"}, Description = "1L stainless steel water bottle.", ImageFile = "bottle1.jpg", Price = 60 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Notebook", Categories = new List<string>{"Office", "Stationery"}, Description = "200-page spiral notebook.", ImageFile = "notebook1.jpg", Price = 30 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Headphones", Categories = new List<string>{"Electronics", "Audio"}, Description = "Over-ear wired stereo headphones.", ImageFile = "headphone1.jpg", Price = 250 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Coffee Mug", Categories = new List<string>{"Home", "Kitchen"}, Description = "Ceramic mug 350ml.", ImageFile = "mug1.jpg", Price = 40 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Phone Case", Categories = new List<string>{"Accessories"}, Description = "Shockproof silicone phone case.", ImageFile = "case1.jpg", Price = 80 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Tablet Holder", Categories = new List<string>{"Accessories", "Home"}, Description = "Flexible tablet holder for bed.", ImageFile = "holder1.jpg", Price = 150 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Power Bank", Categories = new List<string>{"Electronics"}, Description = "10,000mAh fast-charging power bank.", ImageFile = "powerbank1.jpg", Price = 350 },
                new Models.Product { Id = Guid.NewGuid(), Name = "LED Strip", Categories = new List<string>{"Home", "Decor"}, Description = "5m RGB LED strip with remote.", ImageFile = "led1.jpg", Price = 110 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Camping Tent", Categories = new List<string>{"Outdoor", "Travel"}, Description = "Lightweight 2-person camping tent.", ImageFile = "tent1.jpg", Price = 890 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Yoga Mat", Categories = new List<string>{"Sports"}, Description = "Non-slip yoga mat 10mm thick.", ImageFile = "yoga1.jpg", Price = 230 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Electric Kettle", Categories = new List<string>{"Home", "Kitchen"}, Description = "1.7L stainless steel electric kettle.", ImageFile = "kettle1.jpg", Price = 260 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Blender", Categories = new List<string>{"Kitchen", "Home"}, Description = "600W smoothie blender.", ImageFile = "blender1.jpg", Price = 420 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Cushion Pillow", Categories = new List<string>{"Home"}, Description = "Soft cotton cushion 40x40.", ImageFile = "pillow1.jpg", Price = 75 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Electric Fan", Categories = new List<string>{"Home", "Electronics"}, Description = "Portable table fan.", ImageFile = "fan1.jpg", Price = 350 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Smartphone Tripod", Categories = new List<string>{"Photography"}, Description = "Adjustable tripod with phone mount.", ImageFile = "tripod1.jpg", Price = 140 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Action Camera", Categories = new List<string>{"Photography", "Electronics"}, Description = "4K waterproof action camera.", ImageFile = "camera1.jpg", Price = 950 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Perfume", Categories = new List<string>{"Beauty"}, Description = "Long-lasting perfume spray.", ImageFile = "perfume1.jpg", Price = 300 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Makeup Kit", Categories = new List<string>{"Beauty"}, Description = "All-in-one makeup starter kit.", ImageFile = "makeup1.jpg", Price = 200 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Hair Dryer", Categories = new List<string>{"Beauty", "Electronics"}, Description = "Fast-dry ionic hair dryer.", ImageFile = "dryer1.jpg", Price = 310 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Digital Scale", Categories = new List<string>{"Home", "Health"}, Description = "Body weight digital scale.", ImageFile = "scale1.jpg", Price = 180 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Dumbbell Set", Categories = new List<string>{"Sports"}, Description = "10kg adjustable dumbbell set.", ImageFile = "dumbbell1.jpg", Price = 540 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Jump Rope", Categories = new List<string>{"Sports"}, Description = "Adjustable fitness jump rope.", ImageFile = "rope1.jpg", Price = 25 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Resistance Bands", Categories = new List<string>{"Sports"}, Description = "Set of 5 resistance bands.", ImageFile = "bands1.jpg", Price = 90 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Protein Shaker", Categories = new List<string>{"Sports", "Accessories"}, Description = "700ml shaker bottle.", ImageFile = "shaker1.jpg", Price = 50 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Wall Clock", Categories = new List<string>{"Home", "Decor"}, Description = "Minimal modern wall clock.", ImageFile = "clock1.jpg", Price = 160 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Floor Mat", Categories = new List<string>{"Home"}, Description = "Soft anti-slip floor mat.", ImageFile = "mat1.jpg", Price = 95 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Table Cloth", Categories = new List<string>{"Home", "Kitchen"}, Description = "Waterproof dining table cloth.", ImageFile = "cloth1.jpg", Price = 70 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Cookware Set", Categories = new List<string>{"Kitchen"}, Description = "Non-stick 7-piece cookware set.", ImageFile = "cookware1.jpg", Price = 1150 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Cutlery Set", Categories = new List<string>{"Kitchen"}, Description = "24-piece stainless steel cutlery set.", ImageFile = "cutlery1.jpg", Price = 280 },
                new Models.Product { Id = Guid.NewGuid(), Name = "HDMI Cable", Categories = new List<string>{"Electronics", "Cables"}, Description = "High-speed HDMI cable 2m.", ImageFile = "hdmi1.jpg", Price = 60 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Car Phone Holder", Categories = new List<string>{"Car", "Accessories"}, Description = "Dashboard magnetic phone holder.", ImageFile = "carholder1.jpg", Price = 90 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Car Vacuum Cleaner", Categories = new List<string>{"Car", "Electronics"}, Description = "Portable mini car vacuum cleaner.", ImageFile = "carvac1.jpg", Price = 320 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Air Freshener", Categories = new List<string>{"Home", "Car"}, Description = "Long-lasting air freshener.", ImageFile = "fresh1.jpg", Price = 30 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Tool Kit", Categories = new List<string>{"Home", "Tools"}, Description = "Home repair tool kit 32pcs.", ImageFile = "toolkit1.jpg", Price = 480 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Electric Screwdriver", Categories = new List<string>{"Tools", "Electronics"}, Description = "Rechargeable electric screwdriver.", ImageFile = "screwdriver1.jpg", Price = 350 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Mini Projector", Categories = new List<string>{"Electronics", "Home"}, Description = "Portable HD mini projector.", ImageFile = "projector1.jpg", Price = 1200 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Keyboard Wrist Rest", Categories = new List<string>{"Office"}, Description = "Memory foam wrist rest pad.", ImageFile = "wrist1.jpg", Price = 85 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Gaming Chair", Categories = new List<string>{"Office", "Gaming"}, Description = "Ergonomic gaming chair with headrest.", ImageFile = "chair1.jpg", Price = 2600 },
                new Models.Product { Id = Guid.NewGuid(), Name = "Office Desk", Categories = new List<string>{"Office", "Furniture"}, Description = "Modern wooden office desk.", ImageFile = "desk1.jpg", Price = 1950 }
            };
            return products;
        }
    }
}
