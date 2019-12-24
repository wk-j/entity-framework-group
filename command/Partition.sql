WITH ranked_score AS (
  SELECT ROW_NUMBER() OVER (
    PARTITION BY "Course" ORDER BY "Score" DESC
  ) AS rn, * FROM public."Students"
)
SELECT * FROM ranked_score
WHERE rn = 1
ORDER BY "Id";
