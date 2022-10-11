package org.example.models;

import com.fasterxml.jackson.core.JsonProcessingException;
import org.example.serialization.ObjectSerializer;

public class DsaModel {
    /**
     * Serialize object
     */
    public final String toJsonString() throws JsonProcessingException {
        return ObjectSerializer.toJsonString(this);
    }
}
