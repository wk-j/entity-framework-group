SELECT * FROM public."Students"
where ("Course", "Score") in (
	select "Course", max("Score")
    from public."Students"
	group by "Course"
)
