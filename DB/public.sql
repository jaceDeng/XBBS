 

-- ----------------------------
-- Sequence structure for jexus_comments_seq
-- ----------------------------
DROP SEQUENCE "public"."jexus_comments_seq";
CREATE SEQUENCE "public"."jexus_comments_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 9223372036854775807
 START 12
 CACHE 1;
SELECT setval('"public"."jexus_comments_seq"', 12, true);

-- ----------------------------
-- Sequence structure for jexus_forums_fid_seq
-- ----------------------------
DROP SEQUENCE "public"."jexus_forums_fid_seq";
CREATE SEQUENCE "public"."jexus_forums_fid_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 9223372036854775807
 START 11
 CACHE 1;
SELECT setval('"public"."jexus_forums_fid_seq"', 11, true);

-- ----------------------------
-- Sequence structure for jexus_users_uid_seq
-- ----------------------------
DROP SEQUENCE "public"."jexus_users_uid_seq";
CREATE SEQUENCE "public"."jexus_users_uid_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 9223372036854775807
 START 12
 CACHE 1;
SELECT setval('"public"."jexus_users_uid_seq"', 12, true);

-- ----------------------------
-- Table structure for jexus_categories
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_categories";
CREATE TABLE "public"."jexus_categories" (
"cid" int2 NOT NULL,
"pid" int2 NOT NULL,
"cname" varchar(30) COLLATE "default",
"content" varchar(255) COLLATE "default",
"keywords" varchar(255) COLLATE "default",
"ico" varchar(128) COLLATE "default",
"master" varchar(100) COLLATE "default" NOT NULL,
"permit" varchar(255) COLLATE "default",
"listnum" int4,
"clevel" varchar(25) COLLATE "default",
"cord" int2
)
WITH (OIDS=FALSE)

;
COMMENT ON COLUMN "public"."jexus_categories"."cname" IS '分类名称';

-- ----------------------------
-- Records of jexus_categories
-- ----------------------------
INSERT INTO "public"."jexus_categories" VALUES ('1', '0', '主版块', ' ', ' ', null, ' ', '1', '0', null, null);
INSERT INTO "public"."jexus_categories" VALUES ('2', '1', 'Jexus安装部署', ' ', ' ', null, ' ', '1', '1', null, null);
INSERT INTO "public"."jexus_categories" VALUES ('3', '1', 'Mono@Ubuntu', ' ', ' ', null, ' ', '1', '1', null, null);
INSERT INTO "public"."jexus_categories" VALUES ('4', '1', 'Mono@CentOS', ' ', ' ', null, ' ', '1', '1', null, null);
INSERT INTO "public"."jexus_categories" VALUES ('5', '1', '树莓派', ' ', ' ', null, ' ', '1', '1', null, null);

-- ----------------------------
-- Table structure for jexus_comments
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_comments";
CREATE TABLE "public"."jexus_comments" (
"id" int4 DEFAULT nextval('jexus_comments_seq'::regclass) NOT NULL,
"fid" int4 NOT NULL,
"uid" int4 NOT NULL,
"content" text COLLATE "default",
"replytime" timestamptz(6)
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_comments
-- ---------------------------- 

-- ----------------------------
-- Table structure for jexus_favorites
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_favorites";
CREATE TABLE "public"."jexus_favorites" (
"id" int4 NOT NULL,
"uid" int4 NOT NULL,
"favorites" int4 NOT NULL,
"content" text COLLATE "default" NOT NULL
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_favorites
-- ----------------------------

-- ----------------------------
-- Table structure for jexus_forums
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_forums";
CREATE TABLE "public"."jexus_forums" (
"fid" int4 DEFAULT nextval('jexus_forums_fid_seq'::regclass) NOT NULL,
"cid" int2 NOT NULL,
"uid" int4 NOT NULL,
"ruid" int4,
"title" varchar(128) COLLATE "default",
"keywords" varchar(255) COLLATE "default",
"content" text COLLATE "default",
"addtime" timestamptz(6),
"updatetime" timestamptz(6),
"lastreply" timestamptz(6),
"views" int4,
"comments" int2,
"favorites" int8,
"closecomment" int2,
"is_top" int2 NOT NULL,
"is_hidden" int2 NOT NULL,
"ord" int8 NOT NULL
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_forums 
-- ----------------------------
-- Table structure for jexus_links
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_links";
CREATE TABLE "public"."jexus_links" (
"id" int2 NOT NULL,
"name" varchar(100) COLLATE "default",
"url" varchar(200) COLLATE "default",
"logo" varchar(200) COLLATE "default",
"is_hidden" int2 NOT NULL
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_links
-- ----------------------------

-- ----------------------------
-- Table structure for jexus_notifications
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_notifications";
CREATE TABLE "public"."jexus_notifications" (
"nid" int4 NOT NULL,
"fid" int4,
"suid" int4,
"nuid" int4 NOT NULL,
"ntype" int2,
"ntime" int4
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_notifications
-- ----------------------------

-- ----------------------------
-- Table structure for jexus_page
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_page";
CREATE TABLE "public"."jexus_page" (
"pid" int2 NOT NULL,
"title" varchar(100) COLLATE "default",
"content" text COLLATE "default",
"go_url" varchar(100) COLLATE "default",
"add_time" int4,
"is_hidden" int2
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_page
-- ----------------------------

-- ----------------------------
-- Table structure for jexus_settings
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_settings";
CREATE TABLE "public"."jexus_settings" (
"id" int2 NOT NULL,
"title" varchar(255) COLLATE "default" NOT NULL,
"value" text COLLATE "default" NOT NULL,
"type" int2 NOT NULL
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_settings
-- ----------------------------
INSERT INTO "public"."jexus_settings" VALUES ('1', 'site_name', 'Jexus', '0');
INSERT INTO "public"."jexus_settings" VALUES ('2', 'welcome_tip', '欢迎访问Jexus社区', '0');
INSERT INTO "public"."jexus_settings" VALUES ('3', 'short_intro', 'Mono中文社区', '0');
INSERT INTO "public"."jexus_settings" VALUES ('4', 'show_captcha', 'on', '0');
INSERT INTO "public"."jexus_settings" VALUES ('5', 'site_run', '0', '0');
INSERT INTO "public"."jexus_settings" VALUES ('6', 'site_stats', '统计代码', '0');
INSERT INTO "public"."jexus_settings" VALUES ('7', 'site_keywords', '轻量 ?  易用  ?  社区系统', '0');
INSERT INTO "public"."jexus_settings" VALUES ('8', 'site_description', '最火的Mono社区', '0');
INSERT INTO "public"."jexus_settings" VALUES ('9', 'money_title', '银币', '0');
INSERT INTO "public"."jexus_settings" VALUES ('10', 'per_page_num', '20', '0');
INSERT INTO "public"."jexus_settings" VALUES ('11', 'is_rewrite', 'off', '0');
INSERT INTO "public"."jexus_settings" VALUES ('12', 'show_editor', 'off', '0');
INSERT INTO "public"."jexus_settings" VALUES ('13', 'comment_order', 'desc', '0');
INSERT INTO "public"."jexus_settings" VALUES ('14', 'storage_set', 'local', '0');
INSERT INTO "public"."jexus_settings" VALUES ('15', 'auto_tag', 'off', '0');
INSERT INTO "public"."jexus_settings" VALUES ('16', 'sys_url', ' ', '0');

-- ----------------------------
-- Table structure for jexus_tags
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_tags";
CREATE TABLE "public"."jexus_tags" (
"tag_id" int4 NOT NULL,
"tag_title" varchar(30) COLLATE "default" NOT NULL,
"forums" int4 NOT NULL
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_tags
-- ----------------------------

-- ----------------------------
-- Table structure for jexus_tags_relation
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_tags_relation";
CREATE TABLE "public"."jexus_tags_relation" (
"tag_id" int4 NOT NULL,
"fid" int4
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_tags_relation
-- ----------------------------

-- ----------------------------
-- Table structure for jexus_user_follow
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_user_follow";
CREATE TABLE "public"."jexus_user_follow" (
"follow_id" int8 NOT NULL,
"uid" int8 NOT NULL,
"follow_uid" int8 NOT NULL,
"addtime" int4 NOT NULL
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_user_follow
-- ----------------------------

-- ----------------------------
-- Table structure for jexus_user_groups
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_user_groups";
CREATE TABLE "public"."jexus_user_groups" (
"gid" int4 NOT NULL,
"group_type" int2 NOT NULL,
"group_name" varchar(50) COLLATE "default",
"usernum" int4 NOT NULL
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_user_groups
-- ----------------------------
INSERT INTO "public"."jexus_user_groups" VALUES ('1', '0', '管理员', '1');
INSERT INTO "public"."jexus_user_groups" VALUES ('2', '1', '版主', '0');
INSERT INTO "public"."jexus_user_groups" VALUES ('3', '2', '普通会员', '0');

-- ----------------------------
-- Table structure for jexus_users
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_users";
CREATE TABLE "public"."jexus_users" (
"uid" int4 DEFAULT nextval('jexus_users_uid_seq'::regclass) NOT NULL,
"username" varchar(20) COLLATE "default",
"password" char(32) COLLATE "default",
"openid" char(32) COLLATE "default" NOT NULL,
"email" varchar(50) COLLATE "default",
"avatar" varchar(100) COLLATE "default",
"homepage" varchar(50) COLLATE "default",
"money" int4,
"signature" text COLLATE "default",
"forums" int4,
"replies" int4,
"notices" int2,
"follows" int4 NOT NULL,
"regtime" timestamptz(6),
"lastlogin" timestamptz(6),
"lastpost" timestamptz(6),
"qq" varchar(20) COLLATE "default",
"group_type" int2 NOT NULL,
"gid" int2 NOT NULL,
"ip" char(15) COLLATE "default",
"location" varchar(128) COLLATE "default",
"token" varchar(40) COLLATE "default",
"introduction" text COLLATE "default",
"is_active" int2 NOT NULL
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_users
-- ----------------------------
INSERT INTO "public"."jexus_users" VALUES ('4', 'admin', '34F85CA80EC353D3052B8A2D3973A0C5', '                                ', 'abc@xx.com', null, null, '100', ' ', '0', '0', '0', '0', '2014-03-07 20:55:42.548339+08', null, null, ' ', '0', '1', '222.209.110.12 ', ' ', null, ' ', '1'); 

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------
ALTER SEQUENCE "public"."jexus_forums_fid_seq" OWNED BY "jexus_forums"."fid";
ALTER SEQUENCE "public"."jexus_users_uid_seq" OWNED BY "jexus_users"."uid";

-- ----------------------------
-- Primary Key structure for table jexus_categories
-- ----------------------------
ALTER TABLE "public"."jexus_categories" ADD PRIMARY KEY ("cid", "pid");

-- ----------------------------
-- Primary Key structure for table jexus_comments
-- ----------------------------
ALTER TABLE "public"."jexus_comments" ADD PRIMARY KEY ("id", "fid", "uid");

-- ----------------------------
-- Primary Key structure for table jexus_favorites
-- ----------------------------
ALTER TABLE "public"."jexus_favorites" ADD PRIMARY KEY ("id", "uid");

-- ----------------------------
-- Primary Key structure for table jexus_forums
-- ----------------------------
ALTER TABLE "public"."jexus_forums" ADD PRIMARY KEY ("fid", "cid", "uid");

-- ----------------------------
-- Primary Key structure for table jexus_links
-- ----------------------------
ALTER TABLE "public"."jexus_links" ADD PRIMARY KEY ("id");

-- ----------------------------
-- Primary Key structure for table jexus_notifications
-- ----------------------------
ALTER TABLE "public"."jexus_notifications" ADD PRIMARY KEY ("nid", "nuid");

-- ----------------------------
-- Primary Key structure for table jexus_page
-- ----------------------------
ALTER TABLE "public"."jexus_page" ADD PRIMARY KEY ("pid");

-- ----------------------------
-- Primary Key structure for table jexus_settings
-- ----------------------------
ALTER TABLE "public"."jexus_settings" ADD PRIMARY KEY ("id", "title", "type");

-- ----------------------------
-- Primary Key structure for table jexus_tags
-- ----------------------------
ALTER TABLE "public"."jexus_tags" ADD PRIMARY KEY ("tag_id");

-- ----------------------------
-- Primary Key structure for table jexus_user_follow
-- ----------------------------
ALTER TABLE "public"."jexus_user_follow" ADD PRIMARY KEY ("follow_id", "uid", "follow_uid");

-- ----------------------------
-- Primary Key structure for table jexus_user_groups
-- ----------------------------
ALTER TABLE "public"."jexus_user_groups" ADD PRIMARY KEY ("gid", "group_type");

-- ----------------------------
-- Primary Key structure for table jexus_users
-- ----------------------------
ALTER TABLE "public"."jexus_users" ADD PRIMARY KEY ("uid", "group_type");
