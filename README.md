# 🏥 Hệ Thống Quản Lý Thiết Bị Y Tế

## 📖 Tổng Quan

**Hệ Thống Quản Lý Thiết Bị Y Tế** là một ứng dụng web hỗ trợ quản lý thiết bị y tế trong bệnh viện hoặc cơ sở y tế. Ứng dụng cho phép người dùng mượn/trả thiết bị, quản lý danh sách thiết bị, gửi nhắc nhở khi quá hạn, và có phần quản trị viên để giám sát toàn bộ hệ thống. Với giao diện thân thiện và bảo mật cao, hệ thống giúp tối ưu hóa quy trình làm việc và giảm thiểu mất mát thiết bị.

## ✨ Tính Năng

### 👤 Dành cho Người Dùng (Nhân Viên Y Tế)
- **Đăng Ký và Đăng Nhập**: Tạo tài khoản và đăng nhập bằng email/mật khẩu hoặc Google OAuth2.
- **Quản Lý Thiết Bị**: Xem danh sách thiết bị, tìm kiếm theo loại, tình trạng (có sẵn, đang mượn, hỏng).
- **Mượn Thiết Bị**: Chọn thiết bị, ghi nhận thời gian mượn, và xác nhận.
- **Trả Thiết Bị**: Trả thiết bị, cập nhật tình trạng, và ghi nhận thời gian trả.
- **Nhắc Nhở Quá Hạn**: Hệ thống tự động gửi thông báo (email hoặc in-app) khi thiết bị mượn quá hạn.
- **Hồ Sơ Cá Nhân**: Xem lịch sử mượn/trả, thông tin cá nhân, và các nhắc nhở.

### 🛡️ Dành cho Quản Trị Viên
- **Quản Lý Thiết Bị**: Thêm, sửa, xóa thiết bị; cập nhật tình trạng và số lượng.
- **Quản Lý Người Dùng**: Xem danh sách người dùng, cấp quyền, và xử lý tài khoản vi phạm.
- **Giám Sát Mượn Trả**: Xem lịch sử mượn/trả, báo cáo quá hạn, và thống kê sử dụng thiết bị.
- **Báo Cáo**: Tạo báo cáo doanh thu (nếu có phí mượn) hoặc tình hình thiết bị.

## 🧩 Công Nghệ Sử Dụng

### ⚙️ Backend
- **ASP.NET Core 8.0** (hoặc Spring Boot nếu dùng Java)
- **Entity Framework Core** (hoặc Spring Data JPA) cho cơ sở dữ liệu
- **SQL Server** cho dữ liệu quan hệ (thiết bị, lịch sử mượn/trả)

### 🛠️ Công Cụ
- **Git** để quản lý mã nguồn
- **Docker** để container hóa
- **Postman** để kiểm tra API
- **Visual Studio/VS Code** (hoặc IntelliJ nếu dùng Java)

## 📋 Yêu Cầu Cài Đặt
- **.NET SDK 8.0** (hoặc Java JDK 17+ nếu dùng Spring Boot)
- **Node.js 18+** cho ReactJS
- **MySQL 8.0+** (hoặc SQL Server)
- **Docker** (tùy chọn)
- **Git** và **Postman**

## 📂 Cài Đặt và Chạy Dự Án

1. **Clone mã nguồn**:
   ```bash
   git clone https://github.com/leduy2004-coder/WebQLTBYT.NET.git
