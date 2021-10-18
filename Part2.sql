SELECT PlatformName,Id, PlatformId,UniqueName,Latitute, Longitude, CreatedAt, UpdatedAt FROM 
	   (SELECT PlatformName,Id, PlatformId,UniqueName,Latitute, Longitude, CreatedAt, UpdatedAt,ROW_NUMBER() OVER (PARTITION BY PlatformId ORDER BY UpdatedAt DESC) as row_num
       FROM  PlatformInfo WITH (NOLOCK)
	   )
WHERE row_num = 1