SELECT DISTINCT ON ("Course")
       "Id", "Course", "Score"
FROM   public."Students"
ORDER  BY 2, 3 DESC, 1