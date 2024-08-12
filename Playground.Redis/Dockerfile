# docker build -t redis_local .

# docker run --rm -d -p 6380:6379 redis_local

# cat /usr/local/etc/redis/redis.conf | grep -E 'requirepass|rename-command'

FROM redis
COPY redis.conf /usr/local/etc/redis/redis.conf
CMD ["redis-server", "--include /usr/local/etc/redis/redis.conf"]