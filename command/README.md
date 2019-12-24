## Command

- https://stackoverflow.com/questions/3800551/select-first-row-in-each-group-by-group

```
wk-send-command         \
    --user root     \
    --password 1234     \
    --database app \
    --file command/First.sql

wk-send-command         \
    --user root     \
    --password 1234     \
    --database app \
    --file command/All.sql

wk-send-command     \
    --user root     \
    --password 1234 \
    --database app  \
    --file command/Join.sql

wk-send-command     \
    --user root     \
    --password 1234 \
    --database app  \
    --file command/Partition.sql
```