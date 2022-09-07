Select *from Customer where name like 'joe%'

select p.Name 
from Product P
inner join OrderProduct Op on p.productId = op.ProductId
inner join Order o on op.orderId = op.orderId
inner Join Customer c on o.CustomerId = c.customerId
where c.Name like 'joe' and o.CreatedAt > '11/02/2016'


select sum(p.price) as TotalAmount, c.Name
from Product P
inner join OrderProduct Op on p.productId = op.ProductId
inner join Order o on op.orderId = op.orderId
inner Join Customer c on o.CustomerId = c.customerId
Group by c.Name
having c.name like 'joe'

with CTE
as OrderCount 
(
select c.Name,COUNT(o.orderId) as orderCount
from Product P
inner join OrderProduct Op on p.productId = op.ProductId
inner join Order o on op.orderId = op.orderId
inner Join Customer c on o.CustomerId = c.customerId
Group by c.Name, o.ORDERId)

select *from OrderCount where orderCount >1








