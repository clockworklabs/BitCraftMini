use spacetimedb::spacetimedb;

#[spacetimedb(table)]
pub struct AnimationComponent {
    #[unique]
    pub entity_id: u32,
    pub moving: bool,
    pub action_target_entity_id: u32,
}
