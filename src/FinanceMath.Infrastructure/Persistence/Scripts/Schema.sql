CREATE DATABASE finance_math;

CREATE TABLE users (
    id UUID PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    full_name VARCHAR(200) NOT NULL,
    email VARCHAR(200) NOT NULL,
    password_hash VARCHAR(200) NOT NULL,
    type_id INT NOT NULL,
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    updated_at TIMESTAMP WITHOUT TIME ZONE
);

CREATE TABLE categories (
    id UUID PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    parent_category_id UUID NULL,

    CONSTRAINT fk_categories_parent 
    FOREIGN KEY (parent_category_id)
    REFERENCES categories (id)
    ON DELETE RESTRICT
);

CREATE TABLE contents (
    id UUID PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    body TEXT NOT NULL,
    sort_order INT NOT NULL,
    media_url VARCHAR(500),
    is_last_in_module BOOLEAN NOT NULL DEFAULT FALSE,
    category_id UUID NOT NULL,
    created_by UUID NOT NULL,
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    updated_at TIMESTAMP WITHOUT TIME ZONE,

    CONSTRAINT fk_contents_category 
    FOREIGN KEY (category_id) 
    REFERENCES categories (id)
    ON DELETE RESTRICT
);

CREATE TABLE content_sections (
    id UUID PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    body TEXT NOT NULL,
    sort_order INT NOT NULL,
    content_id UUID NOT NULL,

    CONSTRAINT fk_content_sections_content 
    FOREIGN KEY (content_id)
    REFERENCES contents (id)
    ON DELETE CASCADE
);

CREATE TABLE exercises (
    id UUID PRIMARY KEY,
    question TEXT NOT NULL,
    explanation TEXT NOT NULL,
    difficulty VARCHAR(20) NOT NULL,
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMP WITHOUT TIME ZONE
);

CREATE TABLE content_exercises (
    content_id UUID NOT NULL,
    exercise_id UUID NOT NULL,

    PRIMARY KEY (content_id, exercise_id),

    CONSTRAINT fk_content_exercises_content
    FOREIGN KEY (content_id)
    REFERENCES contents (id)
    ON DELETE CASCADE,

    CONSTRAINT fk_content_exercises_exercise
    FOREIGN KEY (exercise_id)
    REFERENCES exercises (id)
    ON DELETE CASCADE
);

CREATE TABLE exercise_options (
    id UUID PRIMARY KEY,
    exercise_id UUID NOT NULL,
    description TEXT NOT NULL,
    sort_order INT NOT NULL,
    is_correct BOOLEAN NOT NULL DEFAULT FALSE,

    CONSTRAINT fk_exercise_options_exercise 
    FOREIGN KEY (exercise_id)
    REFERENCES exercises (id)
    ON DELETE CASCADE
);

CREATE TABLE exercise_hints (
    id UUID PRIMARY KEY,
    exercise_id UUID NOT NULL,
    description TEXT NOT NULL,
    sort_order INT NOT NULL,

    CONSTRAINT fk_exercise_hints_exercise 
    FOREIGN KEY (exercise_id)
    REFERENCES exercises (id)
    ON DELETE CASCADE
);

CREATE TABLE levels (
    id INT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    threshold_experience INT NOT NULL
);

CREATE TABLE gamification_profiles (
    id UUID PRIMARY KEY,
    user_id UUID NOT NULL UNIQUE,
    experience_points INT NOT NULL DEFAULT 0,
    virtual_currency INT NOT NULL DEFAULT 0,
    level_id INT NOT NULL DEFAULT 1,
    current_streak_days INT NOT NULL DEFAULT 0,
    last_activity_date TIMESTAMP WITHOUT TIME ZONE,

    CONSTRAINT fk_gamification_profiles_user
    FOREIGN KEY (user_id)
    REFERENCES users (id)
    ON DELETE CASCADE,

    CONSTRAINT fk_gamification_profiles_level 
    FOREIGN KEY (level_id)
    REFERENCES levels(id)
    ON DELETE RESTRICT
);

CREATE INDEX idx_gamification_profiles_user_id
ON gamification_profiles (user_id);

CREATE TABLE user_exercise_progresses (
    gamification_profile_id UUID NOT NULL,
    exercise_id UUID NOT NULL,
    completed_at TIMESTAMP WITHOUT TIME ZONE NOT NULL,

    PRIMARY KEY (gamification_profile_id, exercise_id),

    CONSTRAINT fk_user_exercise_progresses_gamification_profile
    FOREIGN KEY (gamification_profile_id)
    REFERENCES gamification_profiles (id)
    ON DELETE CASCADE,

    CONSTRAINT fk_user_exercise_progresses_exercise
    FOREIGN KEY (exercise_id)
    REFERENCES exercises (id)
    ON DELETE CASCADE
);

CREATE TABLE user_content_progresses (
    gamification_profile_id UUID NOT NULL,
    content_id UUID NOT NULL,
    completed_at TIMESTAMP WITHOUT TIME ZONE NOT NULL,

    PRIMARY KEY (gamification_profile_id, content_id),

    CONSTRAINT fk_user_content_progresses_gamification_profile
    FOREIGN KEY (gamification_profile_id)
    REFERENCES gamification_profiles (id)
    ON DELETE CASCADE,

    CONSTRAINT fk_user_content_progresses_content
    FOREIGN KEY (content_id)
    REFERENCES contents (id)
    ON DELETE CASCADE
);

CREATE TABLE achievements (
    id UUID PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    description TEXT NOT NULL,
    criteria_key VARCHAR(100) NOT NULL,
    experience_reward INT NOT NULL DEFAULT 0,
    virtual_currency_reward INT NOT NULL DEFAULT 0,
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT NOW()
);

CREATE TABLE user_achievement_progresses (
    gamification_profile_id UUID NOT NULL,
    achievement_id UUID NOT NULL,
    unlocked_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT NOW(),

    PRIMARY KEY (gamification_profile_id, achievement_id),

    CONSTRAINT fk_user_achievement_progresses_gamification_profile 
    FOREIGN KEY (gamification_profile_id)
    REFERENCES gamification_profiles (id)
    ON DELETE CASCADE,

    CONSTRAINT fk_user_achievement_progresses_achievement
    FOREIGN KEY (achievement_id)
    REFERENCES achievements (id)
    ON DELETE CASCADE
);


CREATE TABLE challenges (
    id UUID PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    description TEXT NOT NULL,
    criteria_key VARCHAR(100) NOT NULL,
    target INT NOT NULL DEFAULT 1,
    experience_reward INT NOT NULL DEFAULT 0,
    virtual_currency_reward INT NOT NULL DEFAULT 0,
    start_date TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    end_date TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT NOW()
);

CREATE TABLE user_challenge_progresses (
    gamification_profile_id UUID NOT NULL,
    challenge_id UUID NOT NULL,
    current_progress INT NOT NULL DEFAULT 0,
    target_progress INT NOT NULL,
    is_completed BOOLEAN NOT NULL DEFAULT FALSE,
    started_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT NOW(),
    completed_at TIMESTAMP WITHOUT TIME ZONE,

    PRIMARY KEY (gamification_profile_id, challenge_id),

    CONSTRAINT fk_challenge_progresses_gamification_profile 
    FOREIGN KEY (gamification_profile_id)
    REFERENCES gamification_profiles (id)
    ON DELETE CASCADE,

    CONSTRAINT fk_challenge_progresses_challenge
    FOREIGN KEY (challenge_id)
    REFERENCES challenges (id)
    ON DELETE CASCADE
);

CREATE INDEX idx_user_challenge_progresses_profile_id
ON user_challenge_progresses (gamification_profile_id);
