
--get all Category
create procedure Category_GetAll
as
select* from Category

--------------------------
-- get all food
create procedure Food_GetAll
as
select * from Food

--------------------------
-- get all account
create procedure Account_GetAll
as
select * from Account

-------------------------
-- get all Bill Details
create procedure BillDetails_GetAll
as
select * from BillDetails

-- get all Bills
create procedure Bills_GetAll
as
select * from Bills

-- get all Role
create procedure Role_GetAll
as
select * from Role


-- get all RoleAcc

create procedure RoleAcc_GetAll
as
select * from RoleAccount


-- get all Table
create procedure Table_GetAll
as
select * from [Table]

--------------------------
-- add, delete, update Category
create procedure Category_InsertUpdateDelete
@ID int output,
@Name nvarchar(200),
@Type int,
@Action int
as
	if @Action = 0
	begin
		INSERT INTO [Category] ([Name],[Type])
		VALUES (@Name, @Type)
		SET @ID = @@identity
	end
	else if @Action = 1
	begin
		UPDATE [Category] SET [Name] = @Name, [Type]=@Type WHERE [ID] = @ID
	end
	else if @Action = 2
	begin
		DELETE FROM [Category] WHERE [ID] = @ID
	end
	--drop procedure Category_InsertUpdateDelete
-------------------------
--ad, delete, update food
drop procedure Food_InsertUpdateDelete
create procedure Food_InsertUpdateDelete
@ID int output,
@Name nvarchar(1000),
@Unit nvarchar(100),
@FoodCategoryID int,
@Price int,
@Notes nvarchar(3000),
@Pic nvarchar(3000),
@Action int
AS
	IF @Action = 0 -- Nếu Action = 0, thêm dữ liệu
	BEGIN
		INSERT INTO [Food] ([Name],[Unit],[FoodCategoryID],[Price],[Notes],[Picture])
		VALUES (@Name, @Unit,@FoodCategoryID,@Price,@Notes,@Pic)
		SET @ID = @@identity -- Thiết lập ID tự tăng
	END
	ELSE IF @Action = 1 -- Nếu Action = 1, cập nhật dữ liệu
	BEGIN
		UPDATE [Food]
		SET [Name] = @Name,[Unit]=@Unit,[FoodCategoryID]=@FoodCategoryID,
		[Price]=@Price,[Notes]=@Notes, [Picture] = @Pic
		WHERE [ID] = @ID
	END
	ELSE IF @Action = 2 -- Nếu Action = 2, xóa dữ liệu
	BEGIN
		DELETE FROM [Food] WHERE [ID] = @ID
	END

-- Insert, delete, update account
create procedure Account_InsertUpdateDelete
	@AccountName nvarchar(100),
	@Pass nvarchar(200),
	@FullName nvarchar(1000),
	@Email nvarchar(1000),
	@Tell nvarchar(200),
	@DateCreated smalldatetime,
	@Action int
as
	IF @Action = 0 -- Nếu Action = 0, thêm dữ liệu
	BEGIN
		insert into Account(AccountName,Password, FullName, Email, Tell, DateCreated) 
		values (@AccountName, @Pass, @FullName, @Email, @Tell, @DateCreated)
	END
	ELSE IF @Action = 1 -- Nếu Action = 1, cập nhật dữ liệu
	BEGIN
		update Account
		set AccountName = @AccountName, Password = @Pass, FullName = @FullName, Email = @Email, Tell = @Tell, DateCreated = @DateCreated
		where AccountName = @AccountName
	END
	ELSE IF @Action = 2 -- Nếu Action = 2, xóa dữ liệu
	BEGIN
		Update RoleAccount set Actived = 'false'
		where AccountName = @AccountName
	END

-- insert, delete, update Billdetails
create procedure BillDetails_InsertUpdateDelete
@id int output,
@InvoiceID int,
@FoodID int,
@Quantity int,
@Action int
as
	IF @Action = 0 -- Nếu Action = 0, thêm dữ liệu
	BEGIN
		insert into BillDetails(InvoiceID, FoodID, Quantity) values (@InvoiceID, @FoodID, @Quantity)
		SELECT @id = SCOPE_IDENTITY()
	END
	ELSE IF @Action = 1 -- Nếu Action = 1, cập nhật dữ liệu
	BEGIN
		update BillDetails
		set Quantity = @Quantity
		where ID = @ID and InvoiceID = @InvoiceID
	END
	ELSE IF @Action = 2 -- Nếu Action = 2, xóa dữ liệu
	BEGIN
		Delete BillDetails where ID = @ID
	END

-- insert, delete, update Bills
create procedure Bills_InsertUpdateDelete
@id int output,
@Name nvarchar(1000),
@TableID int,
@Amount int,
@Discount float,
@Tax float,
@Status bit,
@CheckoutDate smalldatetime,
@Account nvarchar(100),
@Action int
as
	IF @Action = 0 -- Nếu Action = 0, thêm dữ liệu
	BEGIN
		insert into Bills(Name, TableID, Amount, Discount, Tax, Status, CheckoutDate, Account) 
		values (@Name, @TableID, @Amount, @Discount, @Tax, @Status,@CheckoutDate, @Account)
		SELECT @id = SCOPE_IDENTITY()
	END
	ELSE IF @Action = 1 -- Nếu Action = 1, cập nhật dữ liệu
	BEGIN
		update [Bills]
		set Name=@Name, @TableID=@TableID, Amount = @Amount, Discount=@Discount, Tax=@Tax, Status=@Status, CheckoutDate=@CheckoutDate,Account=@Account
		where ID = @id
	END
	ELSE IF @Action = 2 -- Nếu Action = 2, xóa dữ liệu
	BEGIN
		Delete BillDetails where InvoiceID = @id
		Delete Bills where ID=@id
	END

-- insert, delete, update role
create procedure Role_InsertUpdateDelete
@ID int output,
@RoleName nvarchar(1000),
@Path nvarchar(3000),
@Notes nvarchar(3000),
@Action int
as
	IF @Action = 0 -- Nếu Action = 0, thêm dữ liệu
	BEGIN
		insert into Role(RoleName,Path,Notes)
		values(@RoleName,@Path,@Notes)
		SELECT @ID = SCOPE_IDENTITY()
	END
	ELSE IF @Action = 1 -- Nếu Action = 1, cập nhật dữ liệu
	BEGIN
		update [Role]
		set RoleName = @RoleName, Path = @Path, Notes = @Notes
		where ID = @ID
	END
	ELSE IF @Action = 2 -- Nếu Action = 2, xóa dữ liệu
	BEGIN
		Delete RoleAccount where RoleID = @ID
		Delete Role where ID=@ID
	END

-- insert, delete, update role acc
create procedure RoleAcc_InsertUpdateDelete
@RoleID int,
@AccountName nvarchar(100),
@Actived bit,
@Notes nvarchar(3000),
@Action int
as
	IF @Action = 0 -- Nếu Action = 0, thêm dữ liệu
	BEGIN
		if (exists (select ID from Role where ID = @RoleID))
		begin
			if(exists (select AccountName from Account where AccountName = @AccountName))
				insert into RoleAccount(RoleID,AccountName, Actived, Notes) values (@RoleID, @AccountName, @Actived, @Notes)
		end
	END
	ELSE IF @Action = 1 -- Nếu Action = 1, cập nhật dữ liệu
	BEGIN
		if(exists (select AccountName from Account where AccountName = @AccountName))
		Update RoleAccount
		set AccountName = @AccountName, Actived = @Actived, Notes = @Notes
		where RoleID = RoleID
	END
	ELSE IF @Action = 2 -- Nếu Action = 2, xóa dữ liệu
	BEGIN
		delete from RoleAccount
		where RoleID = @RoleID
	END

-- insert, delete, update Table
create procedure Table_InsertUpdateDelete
@ID int output,
@Name nvarchar(1000),
@Status int,
@Capacity int,
@Action int
as
	IF @Action = 0 -- Nếu Action = 0, thêm dữ liệu
	BEGIN
		insert into [Table](Name, Status, Capacity) values(@Name, @Status, @Capacity)
		SELECT @ID = SCOPE_IDENTITY()
	END
	ELSE IF @Action = 1 -- Nếu Action = 1, cập nhật dữ liệu
	BEGIN
		update [Table]
		set Name = @Name, Status = @Status, Capacity = @Capacity
		where ID = @ID
	END