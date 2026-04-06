
## C2 – Container Diagram: Dining Hosting System

```
         [Person]                          [Person]
        ┌──────────────┐               ┌──────────────┐
        │   Customer   │               │     Host     │
        │  Browses,    │               │ Creates      │
        │  books dining│               │ offers, menu │
        └──────┬───────┘               └──────┬───────┘
               │ HTTPS                        │ HTTPS
               ▼                              ▼
┌─────────────────────────────────────────────────────────────────┐
│                    Dining Hosting System                        │
│                                                                 │
│              ┌──────────────────────────────┐                  │
│              │        Web Frontend          │                  │
│              │     [React / Next.js]        │                  │
│              │  - UI · JWT storage          │                  │
│              │  - Checkout flow             │                  │
│              └──────────────┬───────────────┘                  │
│                             │ HTTPS + JWT                      │
│                             ▼                                  │
│              ┌──────────────────────────────┐                  │
│              │          API Gateway         │                  │
│              │   [ASP.NET Core + YARP]      │                  │
│              │  - Routing                   │                  │
│              │  - JWT validation            │                  │
│              │  - Rate limiting             │                  │
│              └────┬──────────┬──────────┬───┘                  │
│          HTTPS/REST      HTTPS/REST   HTTPS/REST               │
│                 ▼            ▼            ▼                    │
│   ┌─────────────────┐  ┌──────────────────┐  ┌─────────────┐  │
│   │  User Service   │  │ Product Service  │  │Offer Service│  │
│   │[ASP.NET Core    │  │[ASP.NET Core     │  │[ASP.NET Core│  │
│   │  Web API]       │  │  Web API]        │◄─┤  Web API]   │  │
│   │- Auth           │  │- Food CRUD       │  │- Offers     │  │
│   │- Roles          │  │- Image metadata  │  │- Seats      │  │
│   └────────┬────────┘  └────────┬─────────┘  └──────┬──────┘  │
│            │                   │ HTTPS/REST          │        │
│            │                   │ (image upload)      │        │
│            ▼                   ▼                     ▼        │
│   ┌──────────────┐   ┌───────────────┐   ┌───────────────┐    │
│   │   User DB    │   │  Product DB   │   │   Offer DB    │    │
│   │[PostgreSQL]  │   │ [PostgreSQL]  │   │ [PostgreSQL]  │    │
│   └──────────────┘   └───────┬───────┘   └───────────────┘    │
│                              │ HTTPS                          │
│                              ▼                                │
│
│              ┌──────────────────────────────┐                 │
│              │        Order Service         │                 │
│              │    [ASP.NET Core Web API]    │                 │
│              │  - Booking logic             │                 │
│              │  - Order lifecycle           │                 │
│              │  - Publishes events          │                 │
│              └──────────┬───────────────────┘                 │
│                         │                                     │
│         HTTPS/REST ─────┤                                     │
│    ┌────────────────┐   │   ┌──────────────────┐              │
│    │  User Service  │◄──┤   │  Offer Service   │              │
│    │ (verify user)  │   └──►│ (check seats)    │              │
│    └────────────────┘       └──────────────────┘              │
│                         │                                     │
│              ┌──────────┴───────────┐                         │
│              │                      │                         │
│              ▼                      ▼                         │
│   ┌──────────────────┐   ┌─────────────────────┐              │
│   │    Order DB      │   │      RabbitMQ        │             │
│   │  [PostgreSQL]    │   │  [AMQP Event Bus]    │             │
│   └──────────────────┘   │  - OrderCreated      │             │
│                          │  - PaymentSucceeded  │             │
│                          │  - OrderConfirmed    │             │
│                          └──────────┬───────────┘             │
│                                     │                         │
│                    ┌────────────────┴────────────────┐        │
│                    │ AMQP (subscribe)                 │       │
│                    ▼                                  ▼       │
│        ┌───────────────────────┐   ┌──────────────────────┐  │
│        │   Payment Service     │   │ Notification Service │  │
│        │ [ASP.NET Core Web API]│   │[ASP.NET Core Web API]│  │
│        │ - Payment intent      │   │ - Email              │  │
│        │ - Webhook handling    │   │ - In-app notif.      │  │
│        └───────────┬───────────┘   └──────────┬───────────┘  │
│                    │                          │             │
│          ┌─────────┴──────┐          ┌─────────┴──────┐      │
│          │                │          │                │      │
│          ▼                │          ▼                │      │
│  ┌─────────────┐          │  ┌───────────────┐        │      │
│  │ Payment DB  │          │  │Notification DB│        │      │
│  │[PostgreSQL] │          │  │  [MongoDB]    │        │      │
│  └─────────────┘          │  └───────────────┘        │      │
│                           │                           │      │
└───────────────────────────┼───────────────────────────┼──────┘
                            │ HTTPS/REST                │ SMTP / API
                            ▼                           ▼
               ┌────────────────────┐    ┌─────────────────────┐    ┌──────────────────────┐
               │      Stripe        │    │    SMTP Provider    │    │     Blob Storage     │
               │ [External System]  │    │  [SendGrid / SES]   │    │  [S3 / Azure Blob]   │
               │ - Payment gateway  │    │  [External System]  │    │  [External System]   │
               └─────────┬──────────┘    └─────────────────────┘    │  - Product images    │
                         │ Webhook HTTPS                             └──────────────────────┘
                         ▼
               ┌────────────────────┐
               │   Payment Service  │
               │  (webhook handler) │
               └────────────────────┘
```

### Container Types

| Style | Type |
|---|---|
| `[Person]` | External actor |
| `[React / Next.js]` | Frontend container |
| `[ASP.NET Core Web API]` | Service container |
| `[PostgreSQL]` / `[MongoDB]` | Database container |
| `[AMQP Event Bus]` | Message broker container |
| `[External System]` | External system (outside boundary) |
```
```
