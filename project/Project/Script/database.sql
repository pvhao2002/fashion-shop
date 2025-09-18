USE master;
GO
DROP DATABASE fashion_store;
GO
CREATE DATABASE fashion_store;
GO
USE fashion_store;
GO

CREATE TABLE users
(
    user_id       INT IDENTITY (1,1) PRIMARY KEY,
    full_name     NVARCHAR(100),
    email         NVARCHAR(100) UNIQUE,
    password_hash NVARCHAR(255),
    phone         NVARCHAR(20),
    address       NVARCHAR(MAX),
    avatar        NVARCHAR(MAX),
    status        NVARCHAR(20) DEFAULT 'active'
        CHECK (status IN ('active', 'inactive', 'banned')),
    role          NVARCHAR(20) DEFAULT 'user'
        CHECK (role IN ('user', 'admin')),
    created_at    DATETIME     DEFAULT GETDATE()
);

CREATE TABLE categories
(
    category_id   INT IDENTITY (1,1) PRIMARY KEY,
    category_name NVARCHAR(100),
    image         NVARCHAR(MAX),
    status        NVARCHAR(20) DEFAULT 'active' CHECK (status IN ('active', 'inactive')),
    created_at    DATETIME     DEFAULT GETDATE(),
    updated_at    DATETIME     DEFAULT GETDATE()
);

CREATE TABLE products
(
    product_id  INT IDENTITY (1,1) PRIMARY KEY,
    category_id INT,
    name        NVARCHAR(200),
    description NVARCHAR(MAX),
    price       DECIMAL(10, 2) DEFAULT 0,
    stock       INT            DEFAULT 0,
    status      NVARCHAR(20)   DEFAULT 'active' CHECK (status IN ('active', 'inactive')),
    created_at  DATETIME       DEFAULT GETDATE(),
    updated_at  DATETIME       DEFAULT GETDATE(),
    FOREIGN KEY (category_id) REFERENCES categories (category_id)
);

CREATE TABLE product_images
(
    image_id   INT IDENTITY (1,1) PRIMARY KEY,
    product_id INT           NOT NULL,
    image_url  NVARCHAR(max) NOT NULL,
    is_main    BIT DEFAULT 0,
    FOREIGN KEY (product_id) REFERENCES products (product_id) ON DELETE CASCADE
);

drop table order_items;
drop table orders;
GO
CREATE TABLE orders
(
    order_id         INT IDENTITY (1,1) PRIMARY KEY,
    user_id          INT,
    order_status     NVARCHAR(20)   DEFAULT 'pending'
        CHECK (order_status IN ('pending', 'processing', 'shipped', 'delivered', 'cancelled')),
    payment_status   NVARCHAR(20)   DEFAULT 'unpaid' CHECK (payment_status IN ('unpaid', 'paid')),
    total_amount     DECIMAL(10, 2),
    phone_number     NVARCHAR(15),
    shipping_address NVARCHAR(MAX),
    note             NVARCHAR(MAX),
    payment_method   NVARCHAR(50),
    discount_code    NVARCHAR(50),
    discount_amount  DECIMAL(10, 2) DEFAULT 0,
    created_at       DATETIME       DEFAULT GETDATE(),
    updated_at       DATETIME       DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES users (user_id)
);

CREATE TABLE order_items
(
    order_item_id INT IDENTITY (1,1) PRIMARY KEY,
    order_id      INT,
    product_id    INT,
    quantity      INT,
    price         DECIMAL(10, 2),
    FOREIGN KEY (order_id) REFERENCES orders (order_id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES products (product_id)
);

CREATE TABLE reviews
(
    review_id  INT IDENTITY (1,1) PRIMARY KEY,
    product_id INT,
    user_id    INT,
    rating     INT CHECK (rating BETWEEN 1 AND 5),
    comment    NVARCHAR(MAX),
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (product_id) REFERENCES products (product_id) ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users (user_id) ON DELETE CASCADE
);
GO
DROP TABLE cart_items;
DROP TABLE cart;
GO
CREATE TABLE cart
(
    cart_id     INT IDENTITY (1,1) PRIMARY KEY,
    user_id     INT,
    total_price DECIMAL(17, 2) DEFAULT 0,
    total_items INT            DEFAULT 0,
    created_at  DATETIME       DEFAULT GETDATE(),
    updated_at  DATETIME       DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES users (user_id) ON DELETE CASCADE
);

CREATE TABLE cart_items
(
    cart_item_id     INT IDENTITY (1,1) PRIMARY KEY,
    cart_id          INT,
    product_id       INT,
    quantity         INT CHECK (quantity > 0),
    total_item_price DECIMAL(17, 2) DEFAULT 0,
    FOREIGN KEY (cart_id) REFERENCES cart (cart_id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES products (product_id)
);

CREATE TABLE product_comments
(
    comment_id INT IDENTITY (1,1) PRIMARY KEY,
    product_id INT,
    user_id    INT,
    parent_id  INT,
    content    NVARCHAR(MAX),
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (product_id) REFERENCES products (product_id) ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users (user_id) ON DELETE CASCADE,
    FOREIGN KEY (parent_id) REFERENCES product_comments (comment_id) ON DELETE NO ACTION
);

go
insert into users(full_name, email, password_hash, role)
values (N'Quản trị viên', 'admin@gmail.com', '1234qwer', 'admin');
INSERT INTO categories (category_name, image, status)
VALUES
    (N'Áo sơ mi nam', 'https://product.hstatic.net/1000133495/product/10_60fa8eb3999741179b20d27bcce2317c_master.png', 'active'),
    (N'Áo thun nữ', 'https://bizweb.dktcdn.net/100/386/478/products/a21097-3-1665131401079.jpg?v=1665131477300', 'active'),
    (N'Quần jeans nam', 'https://theblues.com.vn/wp-content/uploads/2023/04/z4304566397022_1a978ce426cf5cf78d74fc0cd3b10d4a.jpg', 'active'),
    (N'Đầm dạ hội', 'https://heradg.vn/media/47669/catalog/Makafds89-33.jpg?size=600', 'active'),
    (N'Áo khoác', 'https://product.hstatic.net/1000369857/product/akd903_1_tui_1200x1200_0000_layer_23_28151cf2d9cb40d7a0088cbea862962c.jpg', 'active'),
    (N'Giày sneaker', 'https://bizweb.dktcdn.net/100/347/092/files/giay-sneaker-la-gi-1.jpg?v=1599104032003', 'active'),
    (N'Túi xách thời trang', 'https://product.hstatic.net/1000084161/product/tui-xach-da-nu-thoi-trang-sang-trong-lata-tx15-nhieu-mau_c2914fb31b0a4fb6aa73c22373fd5128_master.jpg', 'active'),
    (N'Phụ kiện (mũ, kính, thắt lưng)', 'https://hmkeyewear.com/wp-content/uploads/2024/10/phu-kien-thoi-trang-8.jpg', 'active');
