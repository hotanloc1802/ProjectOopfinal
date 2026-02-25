## Classroom Management System

**.NET 8 | WPF (MVVM) | Entity Framework Core | PostgreSQL**

### 1) Tổng quan dự án

**Classroom Management System** là một ứng dụng .NET 8 sử dụng **WPF (MVVM)** và được tổ chức theo hướng **phân lớp kiểu Clean Architecture (inspired)** nhằm đảm bảo tách biệt rõ ràng giữa UI, nghiệp vụ và truy cập dữ liệu.

Mục tiêu của dự án:
- Quản lý dữ liệu lớp học: **Student**, **Teacher**, **Class**, **Assignment**, **Submission**, **Account**, **ClassStudent**
- Thể hiện cách xây dựng ứng dụng C# có cấu trúc, mô hình dữ liệu quan hệ, và kiến trúc dễ mở rộng/bảo trì với **EF Core + PostgreSQL**

---

### 2) Kiến trúc tổng quan (4 layers)

Solution được chia thành 4 lớp logic để đảm bảo **Separation of Concerns** và ranh giới trách nhiệm rõ ràng.

```text
ClassroomManagementApp1.sln

Presentation Layer (WPF)
│
├── Views
├── ViewModels
├── Services (UI context)
└── appsettings.json

src/
├── ClassroomManagement.Domain
│   └── Entities
│
├── ClassroomManagement.Application
│   └── Service Interfaces (Use Cases)
│
├── ClassroomManagement.Infrastructure
│   ├── Persistence (AppDbContext)
│   ├── EF Core Service Implementations
│   └── DependencyInjection
```

#### Presentation Layer (WPF)
**Mục đích**: xử lý giao diện và tương tác người dùng.

**Chứa**:
- XAML Views
- ViewModels (MVVM)
- UI context services
- `appsettings.json`

**Đặc điểm**:
- Không truy cập DB trực tiếp
- Không nhúng SQL/raw query trong UI
- Giao tiếp với nghiệp vụ thông qua **service interfaces** (Application layer)
- Chỉ quản lý state UI, binding, commands

#### Domain Layer
**Mục đích**: định nghĩa **thực thể cốt lõi** (business entities).

**Chứa**:
- Entity classes đại diện các bảng DB: `Student`, `Teacher`, `Class`, `Assignment`, `Submission`, `Account`, `ClassStudent`, ...

**Đặc điểm**:
- POCO thuần C#
- Không phụ thuộc WPF/EF Core
- Mô tả quan hệ dữ liệu qua navigation properties

#### Application Layer
**Mục đích**: định nghĩa **use cases** thông qua **hợp đồng (interfaces)**.

**Chứa**:
- Service interfaces (ví dụ): `IAccountService`, `IClassService`, `IAssignmentService`, `ISubmissionService`, ...

**Trách nhiệm**:
- Chuẩn hoá các thao tác nghiệp vụ mà UI có thể gọi
- Tạo “điểm nối” giữa Presentation và Infrastructure
- Không phụ thuộc EF Core

#### Infrastructure Layer
**Mục đích**: triển khai **data access/persistence** và các chi tiết kỹ thuật.

**Chứa**:
- `AppDbContext` (EF Core)
- Cấu hình DB, mapping entities, định nghĩa relationships/composite keys
- EF Core implementations của Application interfaces (LINQ queries, eager-loading, filtering, ...)
- Dependency Injection (đăng ký DbContext + services)

**Đặc điểm**:
- Phụ thuộc EF Core và Npgsql/PostgreSQL
- Cô lập chi tiết hạ tầng khỏi UI và hợp đồng nghiệp vụ

---

### 3) Trách nhiệm các thành phần

- **Views**
  - Định nghĩa layout UI (XAML)
  - Binding tới ViewModels
  - Không chứa business/data logic

- **ViewModels**
  - Cầu nối UI ↔ Application layer
  - Gọi service interfaces
  - Quản lý state/binding (ObservableCollections, properties) và commands

- **UI Context Services**
  - Giữ session / trạng thái theo ngữ cảnh UI (ví dụ: user đang đăng nhập)
  - Cung cấp dữ liệu ngữ cảnh cho ViewModels

- **Entities (Domain)**
  - Đại diện các bảng trong DB và mô tả quan hệ dữ liệu

- **Service Interfaces (Application)**
  - Định nghĩa các thao tác nghiệp vụ khả dụng
  - Tách UI khỏi cách triển khai dữ liệu cụ thể

- **AppDbContext (Infrastructure)**
  - Khai báo `DbSet<>`
  - Mapping bảng, quan hệ, composite keys

- **EF Core Service Implementations (Infrastructure)**
  - Thực thi các interface bằng LINQ/EF Core
  - Áp dụng lọc dữ liệu, eager loading, quy tắc nghiệp vụ liên quan truy vấn

---

### 4) Luồng xử lý nghiệp vụ (Business Logic Flow)

Một thao tác điển hình:

1. Người dùng tương tác trên UI.
2. View kích hoạt Command trong ViewModel.
3. ViewModel gọi một **Application service interface**.
4. Infrastructure implementation thực thi truy vấn EF Core.
5. PostgreSQL xử lý SQL.
6. Dữ liệu trả về qua service → ViewModel.
7. UI cập nhật qua data binding.

Tóm tắt:

**UI → ViewModel → Service Interface → EF Implementation → Database → ViewModel → UI**

---

### 5) Thiết kế cơ sở dữ liệu

**Database**: PostgreSQL  
**ORM**: Entity Framework Core 8 (Npgsql provider)

#### Các bảng / entities chính
- `Account`: lưu thông tin đăng nhập và role
- `Student`: hồ sơ học sinh
- `Teacher`: hồ sơ giáo viên
- `Class`: thông tin lớp học
- `Assignment`: bài tập (gắn với `Class`), bao gồm metadata và deadline
- `Submission`: bài nộp của học sinh (gắn với `Assignment` và `Student`)
- `ClassStudent`: bảng trung gian many-to-many giữa `Class` và `Student` (composite key)

#### Kiểu quan hệ
- **One-to-Many**
  - Class → Assignment
  - Assignment → Submission
  - Student → Submission
- **Many-to-Many**
  - Student ↔ Class (qua `ClassStudent`)

#### Composite key
- `ClassStudent` sử dụng **composite key** để biểu diễn liên kết many-to-many.

#### LINQ & khái niệm quan hệ
Dự án minh hoạ:
- Filtering: `Where`
- Ordering: `OrderBy`
- Eager loading: `Include`
- Navigation properties
- Join/relational traversal thông qua quan hệ entities

---

### 6) Điểm nổi bật kỹ thuật

- Phân lớp rõ ràng theo hướng Clean Architecture
- Separation of concerns (UI/Business/Data)
- Dependency Injection tập trung tại Infrastructure
- EF Core relational mapping (navigation, relationship, composite key)
- Không dùng raw SQL trong UI
- Cấu hình tách biệt khỏi code (`appsettings.json`)

---

### 7) Tóm tắt

Dự án thể hiện cách tổ chức một ứng dụng .NET theo hướng **dễ bảo trì, dễ mở rộng**:
- UI độc lập khỏi truy cập dữ liệu
- Nghiệp vụ thể hiện qua interfaces rõ ràng
- Infrastructure cô lập chi tiết EF Core/PostgreSQL

Kiến trúc này giúp thuận lợi khi mở rộng tính năng hoặc thay đổi công nghệ persistence mà không làm ảnh hưởng trực tiếp đến UI và hợp đồng nghiệp vụ.
