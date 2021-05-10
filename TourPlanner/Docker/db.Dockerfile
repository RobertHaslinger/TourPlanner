FROM postgres:11.5-alpine
# COPY drop.sql /docker-entrypoint-initdb.d/
COPY init.sql /docker-entrypoint-initdb.d/
